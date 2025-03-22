using Microsoft.EntityFrameworkCore;
using Trade.Domain;

namespace Trade.Infrastructure.Repositories
{
    public class AddressesRepository
    {
        private readonly Context _context;

        public AddressesRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Address>> GetAll(Guid userId)
        {
            return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<Address?> GetById(Guid addressId)
        {
            return await _context.Addresses.FindAsync(addressId);
        }

        public async Task<Address> Add(Guid userId, AddAddressDTO newAddress)
        {
            Address address = new Address(
                userId,
                newAddress.Country,
                newAddress.Region,
                newAddress.City,
                newAddress.PostalCode,
                newAddress.Street,
                newAddress.HouseNumber,
                newAddress.FlatNumber,
                newAddress.AddressDescription);

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> Update(ChangeAddressDTO changeAddress)
        {
            Address? existAddress = await _context.Addresses.FindAsync(changeAddress.Id);

            if (existAddress != null)
            {
                existAddress.Country = changeAddress.Country;
                existAddress.Region = changeAddress.Region;
                existAddress.City = changeAddress.City;
                existAddress.PostalCode = changeAddress.PostalCode;
                existAddress.Street = changeAddress.Street;
                existAddress.HouseNumber = changeAddress.HouseNumber;
                existAddress.FlatNumber = changeAddress.FlatNumber;
                existAddress.AddressDescription = changeAddress.AddressDescription;

                await _context.SaveChangesAsync();
                return existAddress;
            }

            return null;
        }

        public async Task Delete(Guid addressId)
        {
            Address? address = await _context.Addresses.FindAsync(addressId);

            if (address != null)
            {
                _context.Remove(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}
