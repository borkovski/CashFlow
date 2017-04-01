﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CashFlow.BusinessObjects;
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
