﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
    }
}
