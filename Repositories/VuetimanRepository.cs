using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using vuetiman_c.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace vuetiman_c.Repositories
{
    public class VuetimanRepository
    {
        private readonly IDbConnection _db;

        public VuetimanRepository(IDbConnection db)
        {
            _db = db;
        }

        // Find One Find Many add update delete
        public IEnumerable<Vuetiman> GetAll()
        {
            return _db.Query<Vuetiman>("SELECT * FROM Vuetimans");
        }

        public Vuetiman GetById(int id)
        {
            return _db.QueryFirstOrDefault<Vuetiman>($"SELECT * FROM Vuetimans WHERE id = @id", id);
        }

        public Vuetiman Add(Vuetiman Vuetiman)
        {

            int id = _db.ExecuteScalar<int>("INSERT INTO Vuetimans (Name, Description, Price)"
                        + " VALUES(@Name, @Description, @Price); SELECT LAST_INSERT_ID()", new
                        {
                            Vuetiman.Name,
                            Vuetiman.Description,
                            Vuetiman.Price
                        });
            Vuetiman.Id = id;
            return Vuetiman;

        }

        public Vuetiman GetOneByIdAndUpdate(int id, Vuetiman Vuetiman)
        {
            return _db.QueryFirstOrDefault<Vuetiman>($@"
                UPDATE Vuetimans SET  
                    Name = @Name,
                    Description = @Description,
                    Price = @Price
                WHERE Id = {id};
                SELECT * FROM Vuetimans WHERE id = {id};", Vuetiman);
        }

        public string FindByIdAndRemove(int id)
        {
            var success = _db.Execute(@"
                DELETE FROM Vuetimans WHERE Id = @id
            ", id);
            return success > 0 ? "success" : "umm that didnt work";
        }
    }
}
