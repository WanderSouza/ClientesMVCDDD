﻿using ClientesMVC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesMVC.Domain.Interfaces.Repositories
{
    //Herda os métodos CRUD da interface base
    public interface IClientesRepository : IRepositoryBase<Clientes>
    {
        
    }
}
