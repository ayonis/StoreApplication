﻿using Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Store.Services
{
    public class ItemService : IBasicServices<Item>
    {
        protected Store_DB context;
        public ItemService( Store_DB contxt) 
        { 
            context = contxt;
        }
        public List<Item> GetAll()
        {
            var Items = context.Items.ToList();
            return Items;
        }

        public Item GetRecordById(int id)
        {
            var item = context.Items.AsNoTracking().SingleOrDefault( i => i.Id==id);
            return item;
        }
        public int AddRecord(Item record)
        {
            if(record is null) 
            {
                return -1;
            }
            else
            {
                context.Items.Add(record);
                context.SaveChanges();
                return 1;
            }
          
        }
        public short UpdateRecord(Item record)
        {
            var existingRecord = GetRecordById(record.Id);

                if(record is null || existingRecord is null) return -1;

                else
                {
                    context.Items.Update(record);
                    context.SaveChanges();
                    return 1;
                }
            
        }

        public short DeleteRecord(int id) {

            var existingRecord = GetRecordById(id);

            if (existingRecord == null)
            {
                return -1;   
            }
            else
            {
                context.Items.Remove(existingRecord);
                context.SaveChangesAsync();
                return 1;
            }

        }

        public void UpdateItemQuantity(int ItemId , int quantity) 
        {
            var Item = GetRecordById(ItemId);
            Item.Quantity += quantity;

            UpdateRecord(Item);

        }

        public List<Item> FindRecordsByCondition(Func<Item, bool> predicate)
        {
            
            return context.Items.Where(predicate).ToList();
            
        }
    }
}
