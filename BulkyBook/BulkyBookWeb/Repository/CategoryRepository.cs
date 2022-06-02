using BulkyBookWeb.Repository.IRepository;
using BulkyBookWeb.Models;
using System.Linq.Expressions;
using BulkyBookWeb.Data;

namespace BulkyBookWeb.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
