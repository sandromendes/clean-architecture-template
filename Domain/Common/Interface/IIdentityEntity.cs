using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Interface
{
    public interface IIdentityEntity
    {
        public Guid Id { get; set; }
    }
}
