using Dapper;
using Dapper.FastCrud;
using Npgsql;

namespace Repository.Repository
{
    public class Postgre<T> where T : class, new ()
    {
        private readonly NpgsqlConnection _conn;

        public Postgre(string connstr)
        {
            OrmConfiguration.DefaultDialect = SqlDialect.PostgreSql;
            _conn = new NpgsqlConnection(connstr);
        }

        protected NpgsqlConnection GetConnection()
        {
            return _conn;
        }

        public dynamic Insert(T item)
        {
            dynamic result;
            try
            {
                _conn.Open();
                _conn.Insert(item);
                result = item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public bool Update(T item)
        {
            bool result;
            try
            {
                _conn.Open();
                result = _conn.Update(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public bool Delete(T item)
        {
            bool result;
            try
            {
                _conn.Open();
                result = _conn.Delete(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public List<T> GetAll()
        {
            List<T> result;
            try
            {
                _conn.Open();
                result = _conn.Find<T>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public T ItemQuery(string query)
        {
            T result;
            try
            {
                _conn.Open();
                result = _conn.Query<T>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public List<T> ListQuery(string query)
        {
            List<T> result;
            try
            {
                _conn.Open();
                result = _conn.Query<T>(query).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }

        public int CountQuery(string query)
        {
            int result;
            try
            {
                _conn.Open();
                result = _conn.Query<int>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                _conn.Close();
            }
            return result;
        }
    }
}
