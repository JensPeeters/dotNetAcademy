using Data_layer.Interfaces;
using Data_layer.Model;
using System.Linq;

namespace Data_layer.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DatabaseContext _context;
        public AdminRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public Admin GetAdminByID(string adminId)
        {
            var admin = _context.Admins.FirstOrDefault(d => d.AzureId == adminId);

            if (admin == null)
                return null;

            return admin;
        }
        public Admin CreateAdmin(Admin admin)
        {
            var existingAdmin = _context.Admins.FirstOrDefault(d => d.AzureId == admin.AzureId);

            if (existingAdmin == null)
                return null;

            _context.Admins.Add(admin);

            return admin;
        }

        public void SaveChanges()
        {
            if (_context.SaveChanges() > 0)
            {
                _context.SaveChanges();
            }
        }
    }
}
