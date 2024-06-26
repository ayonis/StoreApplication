﻿using Store.Interfaces;
using Store.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Store.Services
{
    public class CategoryService : IBasicServices<Category>
    {
        protected Store_DB context;

        public CategoryService( Store_DB contxt)
        {
            context = contxt;
        }
        public int AddRecord(Category record)
        {
            if (record is null)
            {
                return -1;
            }
            else
            {
                context.Categories.Add(record);
                context.SaveChanges();
                return 1;
            }
        }

        public short DeleteRecord(int id)
        {
            var existingRecord = GetRecordById(id);

            if (existingRecord == null)
            {
                return -1;
            }
            else
            {
                context.Categories.Remove(existingRecord);
                context.SaveChangesAsync();
                return 1;
            }
        }

        public List<Category> GetAll()
        {
            var Categories = context.Categories.ToList();
            return Categories;
        }

        public Category GetRecordById(int id)
        {
            var Category = context.Categories.AsNoTracking().SingleOrDefault(c => c.Id == id);
            return Category;
        }

        public short UpdateRecord(Category record)
        {
            var existingRecord = GetRecordById(record.Id);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.Categories.Update(record);
                context.SaveChanges();
                return 1;
            }
        }
        public List<Category> FindRecordsByCondition(Func<Category, bool> predicate)
        {
            return context.Categories.Where(predicate).ToList();
        }
    }
}
