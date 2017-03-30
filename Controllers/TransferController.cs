﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects;
using CashFlow.DataAccess.EF;
using BusinessObjects.Enums;
using CashFlow.BusinessLogic;

namespace CashFlow.Controllers
{
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        // GET: api/values
        [HttpGet]
        public List<TransferDto> Get()
        {
            return new TransferLogic().GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TransferDto Get(long id)
        {
            return new TransferLogic().GetById(id);
        }

        // POST api/values
        [HttpPost]
        public long? Post([FromBody]TransferDto transfer)
        {
            return new TransferLogic().Save(transfer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            new TransferLogic().Delete(id);
        }
    }
}
