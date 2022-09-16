using Dapper;
using Dapper.FastCrud;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace Repository.Repository
{
    public class MsSql<T> where T : class, new()
    {
        private readonly SqlConnection _conn;
        public MsSql()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MsSql;
            _conn = new SqlConnection(DbConfiguration.MsSqlConnectionString);
        }

        public MsSql(string connStr)
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MsSql;
            _conn = new SqlConnection(connStr);
        }
        protected SqlConnection GetConnection()
        {
            return _conn;
        }

        public dynamic Insert(T item)
        {
            dynamic result = null;
            try
            {
                _conn.Open();
                _conn.Insert(item);
                result = item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public bool Update(T item)
        {
            var result = false;
            try
            {
                _conn.Open();
                result = _conn.Update(item);
            }
            catch (Exception ex)
            {
                var exstr = ex.Message;
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public bool Delete(T entity)
        {
            var result = false;

            try
            {
                _conn.Open();
                result = _conn.Delete<T>(entity);
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
                throw;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public List<T> GetAll()
        {
            var result = new List<T>();
            try
            {
                _conn.Open();
                result = _conn.Find<T>().ToList();
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public T ItemQuery(string query)
        {
            var result = new T();
            try
            {
                _conn.Open();
                result = _conn.Query<T>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public T ItemQuery(DapperQueryObject queryObject)
        {
            var result = new T();
            DynamicParameters dynamicParameters = new();
          
            for (int i = 0; i < queryObject._params.Count; i++)
            {
                var item = queryObject._params[i];
                dynamicParameters.Add(item.parameterName, item.value, item.DbType);
            }
                
            try
            {
                _conn.Open();

                result = _conn.Query<T>(queryObject.query, dynamicParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public List<T> ListQuery(string query)
        {
            var result = new List<T>();
            try
            {
                _conn.Open();
                result = _conn.Query<T>(query).ToList();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public List<T> ListQuery(DapperQueryObject queryObject)
        {
            var result = new List<T>();
            DynamicParameters dynamicParameters = new();

            for (int i = 0; i < queryObject._params.Count; i++)
            {
                var item = queryObject._params[i];
                dynamicParameters.Add(item.parameterName, item.value, item.DbType);
            }
            try
            {
                _conn.Open();
                result = _conn.Query<T>(queryObject.query, dynamicParameters).ToList();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public int CountQuery(string query)
        {
            var result = 0;
            try
            {
                _conn.Open();
                result = _conn.Query<int>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public int CountQuery(DapperQueryObject queryObject)
        {
            var result = 0;
            DynamicParameters dynamicParameters = new();

            for (int i = 0; i < queryObject._params.Count; i++)
            {
                var item = queryObject._params[i];
                dynamicParameters.Add(item.parameterName, item.value, item.DbType);
            }

            try
            {
                _conn.Open();
                result = _conn.Query<int>(queryObject.query, dynamicParameters).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public static List<G> CustomQuery<G>(string query)
        {
            var result = new List<G>();
            var conn = new SqlConnection(DbConfiguration.MsSqlConnectionString);
            try
            {
                conn.Open();
                result = conn.Query<G>(query).ToList();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static List<G> CustomQuery<G>(DapperQueryObject queryObject)
        {
            var result = new List<G>();


            DynamicParameters dynamicParameters = new();

            for (int i = 0; i < queryObject._params.Count; i++)
            {
                var item = queryObject._params[i];
                dynamicParameters.Add(item.parameterName, item.value, item.DbType);
            }


            var conn = new SqlConnection(DbConfiguration.MsSqlConnectionString);
            try
            {
                conn.Open();
                result = conn.Query<G>(queryObject.query, dynamicParameters).ToList();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static int JustRunQuery(string query)
        {
            int result = 0;
            var conn = new SqlConnection(DbConfiguration.MsSqlConnectionString);
            try
            {
                conn.Open();
                result = conn.Execute(query);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static int JustRunQuery(DapperQueryObject queryObject)
        {
            int result = 0;


            DynamicParameters dynamicParameters = new();

            for (int i = 0; i < queryObject._params.Count; i++)
            {
                var item = queryObject._params[i];
                dynamicParameters.Add(item.parameterName, item.value, item.DbType);
            }

            var conn = new SqlConnection(DbConfiguration.MsSqlConnectionString);
            try
            {
                conn.Open();
                result = conn.Execute(queryObject.query, dynamicParameters);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
