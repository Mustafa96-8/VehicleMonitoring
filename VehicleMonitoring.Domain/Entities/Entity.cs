using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleMonitoring.Domain.Entities
{
    public abstract class Entity<TPrimatyKey>
    {
        public TPrimatyKey Id { get; set; }
    }

    public abstract class Entity : Entity<int>
    {

    }
}
