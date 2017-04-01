using CashFlow.BusinessObjects;
using CashFlow.DataAccess.EF;
using CashFlow.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace CashFlow.BusinessLogic
{
    public class TransferSchemaLogic
    {
        public List<TransferSchemaDto> GetAll()
        {
            using (var context = new CashFlowContext())
            {
                List<TransferSchemaDto> transferSchemas = context.TransferSchema.AsEnumerable().Select<DataAccess.EF.TransferSchema, TransferSchemaDto>(t => TransferSchemaMapper.Map(t)).ToList();
                return transferSchemas;
            }
        }

        public TransferSchemaDto GetById(long id)
        {
            using (var context = new CashFlowContext())
            {
                DataAccess.EF.TransferSchema efTransferSchema = context.TransferSchema.SingleOrDefault(t => t.Id == id);
                if (efTransferSchema != null)
                {
                    TransferSchemaDto transferSchema = TransferSchemaMapper.Map(efTransferSchema);
                    return transferSchema;
                }
                else
                {
                    return null;
                }
            }
        }

        public long? Save(TransferSchemaDto transferSchema)
        {
            if (transferSchema != null)
            {
                using (var context = new CashFlowContext())
                {
                    if (transferSchema.Id.HasValue)
                    {
                        context.TransferSchema.Update(TransferSchemaMapper.Map(transferSchema));
                    }
                    else
                    {
                        TransferSchema transferSchemaMapped = TransferSchemaMapper.Map(transferSchema);
                        context.TransferSchema.Add(transferSchemaMapped);
                        transferSchema.Id = transferSchemaMapped.Id;
                    }
                    context.SaveChanges();
                }
                return transferSchema.Id;
            }
            else
            {
                return null;
            }
        }

        public void Delete(long id)
        {
            using (var context = new CashFlowContext())
            {
                var transferSchema = context.TransferSchema.FirstOrDefault(t => t.Id == id);
                if (transferSchema != null)
                {
                    context.Remove(transferSchema);
                    context.SaveChanges();
                }
            }
        }
    }
}
