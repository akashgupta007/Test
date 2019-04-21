using System;

namespace PoiseERP.Areas.Payroll.Controllers
{
    internal class DataContractJsonSerializer
    {
        private Type type;

        public DataContractJsonSerializer(Type type)
        {
            this.type = type;
        }
    }
}