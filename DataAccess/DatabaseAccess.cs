using Dapper;
using DataAccess.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;

namespace DataAccess
{
    public class DatabaseAccess
    {
        private readonly string _connectionString;

        public DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User?> ValidateCredentialsAsync(string id, string password)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select * " +
                    "from company.users " +
                    "where id like :Id";

                var user = await conn.QuerySingleOrDefaultAsync<User>(query, new {Id = id});

                if (user == null)
                {
                    return null;
                }

                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
            }
        }

        public async Task ChangePasswordAsync(string id, string newPassword)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string sql = "update company.users" +
                    "set passwordHash = :NewPassword, temp = 1" +
                    "where id like :Id";

                string hash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                var parameters = new
                {
                    Id = id,
                    NewPassword = hash,
                };

                await conn.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<Vacation>> GetVacationsAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select a.docId, a.docDate, a.startDate, a.endDate, b.name, a.hours " +
                    "from company.vacations a " +
                    "join company.document_types b " +
                    "on a.type = b.type " +
                    "where a.workerId=:WorkerId and " +
                    "order by a.startDate desc";

                var result = await conn.QueryAsync<Vacation>(query, new { WorkerId = workerId, });
                return result.ToList();
            }
        }

        public async Task<string?> GetFullNameAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select name " +
                    "from company.employees " +
                    "where workerId like :WorkerId ";

                return await conn.QuerySingleOrDefaultAsync<string>(query, new { WorkerId = workerId, });
            }
        }

        public async Task<byte[]?> GetPictureAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select photo " +
                    "from company.employees_photos " +
                    "where workerId like :WorkerId ";

                return await conn.QuerySingleOrDefaultAsync<byte[]>(query, new { WorkerId = workerId });
            }
        }

        public async Task<string?> GetJobTitleAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select jobTitle " +
                    "from company.positions " +
                    "where workerId like :WorkerId " +
                    "order by docDate desc";

                return await conn.QueryFirstOrDefaultAsync<string>(query, new { WorkerId = workerId, });
            }
        }

        public async Task<IEnumerable<SickLeave>> GetSickLeaveAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select a.docId, a.docDate, a.startDate, a.endDate, b.name " +
                    "from company.sick_leave a " +
                    "join company.document_types b " +
                    "on a.type = b.type " +
                    "where a.workerId like :WorkerId " +
                    "order by startDate desc";

                var result = await conn.QueryAsync<SickLeave>(query, new { WorkerId = workerId, });
                return result.ToList();
            }
        }

        public async Task<IEnumerable<RewardPenalty>> GetRewardPenaltiesAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select type, docId, docDate, description, details " +
                    "from company.reward_penalty " +
                    "where workerId like :WorkerId " +
                    "order by docDate ";

                var result = await conn.QueryAsync<RewardPenalty>(query, new { WorkerId = workerId, });
                return result.ToList();
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select startDate, endDate, description, name, type " +
                    "from company.courses " +
                    "where workerId like :WorkerId " +
                    "order by startDate desc";

                var result = await conn.QueryAsync<Course>(query, new { WorkerId = workerId, });
                return result.ToList();
            }
        }

        public async Task<IEnumerable<BusinessTrip>> GetBusinessTripsAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select startDate, endDate, docDate, docId, destination, description " +
                    "from company.business_trips " +
                    "where workerId like :WorkerId " +
                    "order by docDate desc";

                var result = await conn.QueryAsync<BusinessTrip>(query, new { WorkerId = workerId, });
                return result.ToList();
            }
        }

        public async Task<IEnumerable<Certification>> GetCertificationsAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select date, name, description " +
                    "from company.certifications " +
                    "where workerId like :WorkerId " +
                    "order by date";

                var result = await conn.QueryAsync<Certification>(query, new { WorkerId = workerId, });
                return result.ToList();
            }
        }

        public async Task<ClothingSize?> GetClothingSizeAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select height, clothSize, underweaSize, headwearSize, shoeSize, winterJacketSize, winterPantsSize, winterSuitSize, woolenSuitSize, canvasSuitSize " +
                    "from company.clothing_size " +
                    "where code = 0 and workerId like :WorkerId ";

                return await conn.QuerySingleOrDefaultAsync<ClothingSize>(query, new { WorkerId = workerId, });
            }
        }

        public async Task<IEnumerable<Earning>> GetEarningsAsync(string workerId, string yearMonth)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select b.name, a.days, a.hours, a.amount " +
                    "from company.earnings a " +
                    "left join company.earning_types b " +
                    "on b.code = a.code " +
                    "where a.workerId like :WorkerId and " +
                    "a.yearMonth like :YearMonth " +
                    "order by a.code";

                var parameters = new
                {
                    WorkerId = workerId,
                    YearMonth = yearMonth,
                };

                var result = await conn.QueryAsync<Earning>(query, parameters);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<Deduction>> GetDeductionsAsync(string workerId, string yearMonth)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select name, amount " +
                    "from company.deductions " +
                    "where workerId like :WorkerId and " +
                    "yearMonth like :YearMonth " +
                    "order by code";

                var parameters = new
                {
                    WorkerId = workerId,
                    YearMonth = yearMonth,
                };

                var result = await conn.QueryAsync<Deduction>(query, parameters);
                return result.ToList();
            }
        }

        public async Task<decimal> GetWorkDeskIdAsync(string workerId, DateTime date)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "company.employee.get_work_desk_id";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var returnParam = new OracleParameter("return_value", OracleDbType.Decimal);
                    returnParam.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParam);

                    var workerIdParam = new OracleParameter("v_workerId", OracleDbType.Char);
                    workerIdParam.Direction = ParameterDirection.Input;
                    workerIdParam.Value = workerId;
                    cmd.Parameters.Add(workerIdParam);

                    var dateParam = new OracleParameter("v_date", OracleDbType.Date);
                    dateParam.Direction = ParameterDirection.Input;
                    dateParam.Value = date.Date;
                    cmd.Parameters.Add(dateParam);                    

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();

                    return ((OracleDecimal)returnParam.Value).Value;
                }
            }
        }

        public async Task<decimal> GetJobGradeAsync(string workerId, decimal workDeskId, DateTime date)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "company.employee.get_job_grade";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var returnParam = new OracleParameter("return_value", OracleDbType.Decimal);
                    returnParam.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParam);

                    var workerIdParam = new OracleParameter("v_workerId", OracleDbType.Char);
                    workerIdParam.Direction = ParameterDirection.Input;
                    workerIdParam.Value = workerId;
                    cmd.Parameters.Add(workerIdParam);

                    var workDeskIdParam = new OracleParameter("v_workDeskId", OracleDbType.Decimal);
                    workDeskIdParam.Direction = ParameterDirection.Input;
                    workDeskIdParam.Value = workDeskId;
                    cmd.Parameters.Add(workDeskIdParam);

                    var dateParam = new OracleParameter("v_date", OracleDbType.Date);
                    dateParam.Direction = ParameterDirection.Input;
                    dateParam.Value = date.Date;
                    cmd.Parameters.Add(dateParam);

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();

                    return ((OracleDecimal)returnParam.Value).Value;
                }
            }
        }

        public async Task<decimal> GetWageAsync(decimal workDeskId, decimal grade, DateTime date)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                using (var cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "company.employee.get_wage";
                    cmd.CommandType = CommandType.StoredProcedure;

                    var returnParam = new OracleParameter("return_value", OracleDbType.Decimal);
                    returnParam.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParam);

                    var workDeskIdParam = new OracleParameter("v_workDeskId", OracleDbType.Decimal);
                    workDeskIdParam.Direction = ParameterDirection.Input;
                    workDeskIdParam.Value = workDeskId;
                    cmd.Parameters.Add(workDeskIdParam);

                    var gradeParam = new OracleParameter("v_grade", OracleDbType.Decimal);
                    gradeParam.Direction = ParameterDirection.Input;
                    gradeParam.Value = grade;
                    cmd.Parameters.Add(gradeParam);

                    var dateParam = new OracleParameter("v_date", OracleDbType.Date);
                    dateParam.Direction = ParameterDirection.Input;
                    dateParam.Value = date.Date;
                    cmd.Parameters.Add(dateParam);

                    conn.Open();
                    await cmd.ExecuteNonQueryAsync();

                    return ((OracleDecimal)returnParam.Value).Value;
                }
            }
        }

        public async Task<decimal?> GetVacationAccumulatedAsync(string workerId)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                string query = "select days " +
                    "from company.vacation_accumulated " +
                    "where workerId = :WorkerId " +
                    "order by to_date(yearMonth, 'yyyymm') desc";

                return await conn.QueryFirstOrDefaultAsync<decimal>(query, new { WorkerId = workerId });
            }
        }
    }
}
