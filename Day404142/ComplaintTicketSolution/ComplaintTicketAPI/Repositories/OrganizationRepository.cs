using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

// all done 

namespace ComplaintTicketAPI.Repositories
{
    public class OrganizationRepository : IRepository<int, Organization>
    {
        private readonly ComplaintTicketContext _context;

        public OrganizationRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<Organization> Add(Organization entity)
        {
            try
            {
                var addedOrganization = await _context.Organizations.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Failed to add Organization");
            }
        }

        public async Task<Organization> Get(int key)
        {
            try
            {
                return await _context.Organizations.FirstOrDefaultAsync(o => o.orgId == key);
            }
            catch
            {
                throw new InvalidOperationException("NO organization found");
            }

        }

        public async Task<IEnumerable<Organization>> GetAll()
        {


            try
            {

                return await _context.Organizations.ToListAsync();
            }
            catch
            {
                throw new InvalidOperationException("NO organization found");
            }

        }

        public async Task<Organization> Update(Organization entity, int key)
        {
            var organization =  await _context.Organizations.FirstOrDefaultAsync(o => o.UserId == key);
            if (organization != null)
            {
                try
                {
                    organization.Name = entity.Name;
                    organization.Email = entity.Email;
                    organization.Phone = entity.Phone;
                    organization.Address = entity.Address;
                    organization.Types = entity.Types;

                    await _context.SaveChangesAsync();
                    return organization;
                }
                catch
                {
                    throw new InvalidOperationException("NOt able to update");
                }

            }
            throw new CouldNotUpdateException("Organization not found");
        }

        public async Task<Organization> Delete(int key)
        {

            var organization = await Get(key);
            if (organization != null)
            {
                try
                {
                    _context.Organizations.Remove(organization);
                    await _context.SaveChangesAsync();
                    return organization;
                }
                catch
                {
                    throw new InvalidOperationException("NOt able to delete");
                }


            }
            throw new CouldNotDeleteException("Organization not found");
        }
    }
}
