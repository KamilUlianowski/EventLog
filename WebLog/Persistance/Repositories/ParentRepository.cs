﻿using System.Data.Entity;
using System.Linq;
using WebLog.Core.Models;
using WebLog.Core.Repositories;

namespace WebLog.Persistance.Repositories
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        private readonly LogDbContext _context;

        public ParentRepository(LogDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddChildren(int parentId, Student student)
        {
            var parent = _context.Parents.Include(x => x.MyChildrens).FirstOrDefault(x => x.Id == parentId);

            if (parent != null && !parent.MyChildrens.Contains(student))
                parent.MyChildrens.Add(student);
        }

        public override Parent Get(int id)
        {
            return _context.Parents.Include(x => x.MyChildrens)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}