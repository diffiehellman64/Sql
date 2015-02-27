using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class ConditionBuilder
    {
        private List<Condition> _conditionsList;

        public ConditionBuilder()
        {
            _conditionsList = new List<Condition>();
        }

        public void AddCondition(string field,
                            string value,
                            ConditionType condType,
                            Logic logic,
                            ValueType valType,
                            int detph)
        {
            Condition cond = new Condition(field, value, condType, logic, valType, detph);
            _conditionsList.Add(cond);
        }

        public void AddCondition(Condition cond)
        {
            _conditionsList.Add(cond);
        }

        public void AddCondition(int depth, Condition cond)
        {
            cond.depth = depth;
            _conditionsList.Add(cond);
        }

        public string Build()
        {
            int depth = 0;
            StringBuilder conditions = new StringBuilder();
            foreach (Condition cond in _conditionsList)
            {
                if (cond.depth > depth)
                    conditions.Append(cond.Build("down"));
                if (cond.depth < depth)
                    conditions.Append(cond.Build("up"));
                if (cond.depth == depth)
                    conditions.Append(cond.Build());
                conditions.Append(" ");
                depth = cond.depth;
            }
            string conditionsStr = conditions.ToString();
            int space = conditionsStr.IndexOf(" ");
            return conditionsStr.Substring(space).Trim();
        }
    }
}
