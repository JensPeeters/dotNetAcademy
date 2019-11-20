using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_layer.Repositories
{
    public class AdminRepository
    {
        private readonly DatabaseContext context;
        public AdminRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public Admin GetAdminByID(string adminId)
        {
            var admin = context.Admins.FirstOrDefault(d => d.AzureId == adminId);

            if (admin == null)
                return null;

            return admin;
        }
        public Admin CreateAdmin(Admin admin)
        {
            var existingAdmin = context.Admins.FirstOrDefault(d => d.AzureId == admin.AzureId);

            if (existingAdmin == null)
                return null;

            context.Admins.Add(admin);

            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return admin;
        }

        private void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
