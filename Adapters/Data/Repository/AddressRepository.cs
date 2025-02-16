using Dapper;
using Domain.Address;
using Domain.Contracts;
using Domain.Ports;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CustomerDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        private readonly string _databaseConnection;

        public AddressRepository(CustomerDbContext context, IConfiguration configuration)
        {
            _context = context;
            _databaseConnection = configuration.GetConnectionString("Main");
        }

        public async Task<Address> AddAsync(Address address)
        {
            using (var connection = new SqlConnection(_databaseConnection))
            {
                try
                {
                    connection.Open();

                    var sql = "SPI_Address";

                    await connection.ExecuteAsync(sql, new { customerId = address.CustomerId, place = address.Place }, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return address;
        }

        public async Task<Address> UpdateAsync(Address address)
        {
            using (var connection = new SqlConnection(_databaseConnection))
            {
                try
                {
                    connection.Open();

                    var sql = "SPU_Address";

                    await connection.ExecuteAsync(sql, new { addressId = address.Id, place = address.Place }, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return address;
        }

        public async Task<bool> DeleteAsync(Address address)
        {
            using (var connection = new SqlConnection(_databaseConnection))
            {
                try
                {
                    connection.Open();

                    var sql = "SPD_Address";

                    await connection.ExecuteAsync(sql, new { addressId = address.Id }, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return true;
        }

        public async Task<Address> GetByIdAsync(int addressId)
        {
            using (var connection = new SqlConnection(_databaseConnection))
            {
                try
                {
                    connection.Open();

                    var sql = "SPS_AddressById";

                    return await connection.QueryFirstOrDefaultAsync<Address>(sql, new { addressId = addressId }, commandType: System.Data.CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<Address>> GetByCustomerIdAsync(int customerId)
        {
            using (var connection = new SqlConnection(_databaseConnection))
            {
                try
                {
                    connection.Open();

                    var sql = "SPS_AddressesByCustomerId";

                    var list = await connection.QueryAsync<Address>(sql, new { customerId = customerId }, commandType: System.Data.CommandType.StoredProcedure);

                    return list.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
