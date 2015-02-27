using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class SqlBuilder
    {
        private SqlType _type;
        private List<string> _fieldsList;
        private List<string> _tablesList;
        private ConditionBuilder _conditionBuilder;

        public SqlBuilder(SqlType type)
        {
            _type = type;
            _fieldsList = new List<string>();
            _tablesList = new List<string>();
            _conditionBuilder = new ConditionBuilder();
        }

        public void Field(string field)
        {
            _fieldsList.Add(field);
        }

        public void SubQuery(SqlBuilder sql)
        {
            _fieldsList.Add("(" + sql.Build() + ")");
        }

        public void Table(string table)
        {
            _tablesList.Add(table);
        }

        public void AddCondition(string f,
                            string v,
                            ConditionType cT = ConditionType.equal,
                            Logic l = Logic.and,
                            ValueType vT = ValueType.sql)
        {
            _conditionBuilder.AddCondition(f, v, cT, l, vT, 0);
        }

        public void AddCondition(int depth,
                            string f,
                            string v,
                            ConditionType cT = ConditionType.equal,
                            Logic l = Logic.and,
                            ValueType vT = ValueType.sql)
        {
            _conditionBuilder.AddCondition(f, v, cT, l, vT, depth);
        }

        private string DelComma(string sql)
        {
            return sql.TrimEnd(',');
        }

        private string ListBuilder(List<string> list)
        {
            StringBuilder listBuilder = new StringBuilder();
            foreach (string item in list)
            {
                listBuilder.Append(item + ",");
            }
            return DelComma(listBuilder.ToString());
        }

        public string Build()
        {
            StringBuilder sql = new StringBuilder();
            switch (_type)
            {
                case SqlType.select: sql.Append("SELECT"); break;
                case SqlType.insert: sql.Append("INSERT"); break;
                case SqlType.update: sql.Append("UPDATE"); break;
                case SqlType.delete: sql.Append("DELETE"); break;
            }
            sql.Append(" ");
            sql.Append(ListBuilder(_fieldsList));
            sql.Append(" ");
            sql.Append("FROM");
            sql.Append(" ");
            sql.Append(ListBuilder(_tablesList));
            sql.Append(" ");
            sql.Append("WHERE");
            sql.Append(" ");
            sql.Append(_conditionBuilder.Build());
            return sql.ToString();
        }
    }
}
