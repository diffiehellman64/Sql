using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class Condition
    {
        private string _field;

        internal string field 
        {
            get { return _field; }
        }

        private string _value;

        internal string value
        {
            get { return _value; }
        }

        private ConditionType _condType;

        internal ConditionType condType
        {
            get { return _condType; }
        }

        private Logic _logic;

        internal Logic logic
        {
            get { return _logic; }
        }

        private ValueType _valType;

        public ValueType valType
        {
            get { return _valType; }
        }

        private int _depth;

        public int depth
        {
            get { return _depth; }
            set { _depth = value; }
        }

        public Condition(string f,
                            string v,
                            ConditionType cT = ConditionType.equal,
                            Logic l = Logic.and,
                            ValueType vT = ValueType.sql,
                            int depth = 0)
        {
            _field = f;
            _value = v;
            _condType = cT;
            _logic = l;
            _valType = vT;
            _condType = cT;
            _depth = depth;
        }

        internal string Build(string move = null)
        {
            StringBuilder condition = new StringBuilder();
            if (move == "up")
                condition.Append(") ");
            switch (_logic)
            {
                case Logic.and: condition.Append("AND"); break;
                case Logic.or: condition.Append("OR"); break;
            }
            condition.Append(" ");
            if (move == "down")
                condition.Append("(");
            condition.Append(_field);
            condition.Append(" ");
            switch (_condType)
            {
                case ConditionType.equal: condition.Append("="); break;
                case ConditionType.notEqual: condition.Append("<>"); break;
                case ConditionType.like: condition.Append("LIKE"); break;
                case ConditionType.notLike: condition.Append("NOT LIKE"); break;
                case ConditionType.more: condition.Append(">"); break;
                case ConditionType.moreOrEqual: condition.Append(">="); break;
                case ConditionType.less: condition.Append("<"); break;
                case ConditionType.lessOrEqual: condition.Append("="); break;
                case ConditionType.between: condition.Append("BETWEEN"); break;
            }
            condition.Append(" ");
            condition.Append(_value);
            return condition.ToString();
        }
    }
}
