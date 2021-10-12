﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.InterfaceRepository;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class CommissionRepository: GenericRepository<ComissionShare>, ICommissionRepository
    {
        public CommissionRepository(ApplicationContext context) : base(context) { }
        public ApplicationContext ApplicationContext
        {
            get { return _context; }
        }
    }
}
