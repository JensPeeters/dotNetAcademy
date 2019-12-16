using Data_layer.Interfaces;
using Data_layer.Model;
using System;
using System.Linq;

namespace Data_layer.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DatabaseContext _context;
        public AdminRepository(DatabaseContext context)
        {
            _context = context;
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

            if (existingAdmin != null)
                return null;

            _context.Admins.Add(admin);

            return admin;
        }

        public Admin DeleteAdmin(string AdminId)
        {
            var deletedAdmin = _context.Admins.FirstOrDefault(a => a.AzureId == AdminId);
            if (deletedAdmin == null)
                return null;
            try
            {
                _context.Admins.Remove(deletedAdmin);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            return deletedAdmin;
        }

        public void SaveChanges()
        {
            try
            {
                var changes = _context.SaveChanges();
                if (changes == 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
