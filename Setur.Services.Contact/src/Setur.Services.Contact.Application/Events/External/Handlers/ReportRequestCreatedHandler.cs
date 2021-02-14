using Setur.Services.Contact.Application.DTO;
using Setur.Services.Contact.Core.Repositories;
using entity = Setur.Services.Contact.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Setur.Services.Contact.Application.Services;

namespace Setur.Services.Contact.Application.Events.External.Handlers
{
    public class ReportRequestCreatedHandler : IEventHandler<ReportRequestCreated>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMessageBroker _messageBroker;
        public ReportRequestCreatedHandler(IContactRepository contactRepository,
                                           IMessageBroker messageBroker)
        {
            _contactRepository = contactRepository;
            _messageBroker = messageBroker;
        }
        public async Task HandleAsync(ReportRequestCreated @event)
        {
            var contacts  = await _contactRepository.GetAsyncByLocation();
            var result = GetReport(contacts);

            await _messageBroker.PublishAsync(new ReportCompleted(@event.ReportRequestId,  result));
        }

        public List<ReportDto> GetReport(IEnumerable<entity.Contact> contacts)
        {
            List<ReportDto> reports = new List<ReportDto>();

            foreach (var item in contacts)
            {
                if (item.ContactInfos.Any(p => p.InfoType == Core.ValueObjects.InfoType.Location))
                {
                    foreach (var contactIfo in item.ContactInfos)
                    {
                        if (contactIfo.InfoType == Core.ValueObjects.InfoType.Location &&
                            !reports.Any(p=>p.Location == contactIfo.InfoContent)
                            )
                        {
                            var reportDto = new ReportDto();
                            reportDto.ContactCount = contacts.Count(p => p.ContactInfos.Any(p => p.InfoContent == contactIfo.InfoContent));
                            var locationContacts = contacts
                                                      .Where(p => p.ContactInfos.Any(p => p.InfoContent == contactIfo.InfoContent)).ToList();
                            int phoneNumberCount = 0;
                            foreach (var locationContact in locationContacts)
                            {
                                var result = locationContact.ContactInfos.Count(p => p.InfoType == Core.ValueObjects.InfoType.PhoneNumber);
                                phoneNumberCount += result;
                            }

                            reportDto.Location = contactIfo.InfoContent;
                            reportDto.PhoneNumberCount = phoneNumberCount;

                            reports.Add(reportDto);
                        }
                    }
                }
            }

            return reports;
        }

    }
}
