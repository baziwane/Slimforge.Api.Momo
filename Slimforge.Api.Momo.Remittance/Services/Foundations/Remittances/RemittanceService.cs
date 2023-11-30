// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Slimforge.Api.Momo.Remittance.Brokers.DateTimes;
using Slimforge.Api.Momo.Remittance.Brokers.MomoApis;
using Slimforge.Api.Momo.Remittance.Models.Services.Foundations.Remittances;
using Slimforge.Api.Momo.Remittance.Models.Services.Foundations.ExternalRemittances;

namespace Slimforge.Api.Momo.Remittance.Services.Foundations.Remittances
{
    internal partial class RemittanceService : IRemittanceService
    {
        private readonly IMomoBroker momoBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public RemittanceService(
            IMomoBroker momoBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.momoBroker = momoBroker;
            this.dateTimeBroker = dateTimeBroker;
        }
       
    }
}