using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mspec_demo
{
    class TitleCalculatorTreeNode : TreeNode<TreeNodeItem>
    {
         readonly Func<int, double> _getDiscountFor;
        public int Remainder;
        public double RemainderPrice;
        string _stringKey;
        double _unitPrice = 8;


        public TitleCalculatorTreeNode(TreeNode<TreeNodeItem> parent,
            TreeNodeItem treeNodeItem, Func<int, double> getDiscountFor, double unitPrice)
            : base(parent, treeNodeItem)
        {
            _unitPrice = unitPrice;
            _getDiscountFor = getDiscountFor;
            _stringKey = GetUniqueStringRepresentation(treeNodeItem.items_to_calculate);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Grouping[");
            foreach (var g in _obj.grouping)
            {
                sb.Append(g + ",");
            }
            sb.Append("] Items[");
            foreach (var g in _obj.items_to_calculate)
            {
                sb.Append(g + ",");
            }
            sb.Append("] ");
            return sb.ToString();
        }

        public double CalculatePrice(IDictionary<string, double> priceLookUp)
        {
      
            if (priceLookUp.ContainsKey(_stringKey))
            {
                var price = _getDiscountFor(_obj.grouping.Count()) * _obj.grouping.Count() * _unitPrice + priceLookUp[_stringKey];
                return price;
            }

            var possibleGroupings = GetPossibleGroupsRecursive(_obj.items_to_calculate);

            if (possibleGroupings.Count() == 0)
            {
                var price = _obj.items_to_calculate.Count() * _unitPrice;
                priceLookUp.Add(_stringKey, price);

                var finalPrice = _getDiscountFor(_obj.grouping.Count()) * _obj.grouping.Count() * _unitPrice + price;
                return finalPrice;
            }
            else 
            {
                foreach (var g in possibleGroupings)
                {
                    var newItemList = Clone(_obj.items_to_calculate);

                    _children.Add(new TitleCalculatorTreeNode(this,
                        new TreeNodeItem { grouping = g, items_to_calculate = ApplyGrouping(newItemList, g) }                        
                        ,_getDiscountFor, _unitPrice));
                }

                var result = double.MaxValue;
                foreach (TitleCalculatorTreeNode child in _children)
                {
                    var temp = child.CalculatePrice(priceLookUp);
                    if (result > temp) 
                    {
                        result = temp;
                    }
                }

                priceLookUp.Add(_stringKey, result);

                var price = _getDiscountFor(_obj.grouping.Count()) * _obj.grouping.Count() * _unitPrice + result;
                return price;
            }
        }

        IList<string> ApplyGrouping(IEnumerable<string> items, IList<string> grouping)
        {
            var list = items.ToList();
            foreach (var t in grouping)
            {
                list.Remove(t);
            }
            return list;
        }

        IEnumerable<string> Clone(IEnumerable<string> titles)
        {
            return new List<string>(titles);
        }

        IEnumerable<IList<string>> GetPossibleGroupsRecursive(IEnumerable<string> items)
        {
            var distinctItems = items.Distinct().Select(title => title);

            if (distinctItems.Count() < 2) return new List<IList<string>>();

            IList<IList<string>> result = new List<IList<string>>();

            PickOutOfTheArray(2, new string[] { }, distinctItems.ToArray(), result);
            PickOutOfTheArray(3, new string[] { }, distinctItems.ToArray(), result);
            PickOutOfTheArray(4, new string[] { }, distinctItems.ToArray(), result);
            PickOutOfTheArray(5, new string[] { }, distinctItems.ToArray(), result);

            return result;

        }

        void PickOutOfTheArray(int size, string[] fixedArray, string[] array, IList<IList<string>> resultList)
        {
            if (size == 1)
            {
                foreach (var s in array)
                {
                    var result = new List<string>();
                    foreach (var t in fixedArray)
                    {
                        result.Add(t);
                    }
                    result.Add(s);
                    resultList.Add(result);
                }
                return;
            }

            for (var i = 0; i < (array.Length - size + 1); i++)
            {
                var s = array[i];
                var newFixedArray = new List<string>(fixedArray);
                newFixedArray.Add(s);
                var newArray = new List<string>(array);
                for (var j = 0; j <= i; j++) newArray.RemoveAt(0);
                PickOutOfTheArray(size - 1, newFixedArray.ToArray(), newArray.ToArray(), resultList);
            }
        }

        string GetUniqueStringRepresentation(IEnumerable<string> titles)
        {
            var counts =
                titles
                .GroupBy(item => item.ToString())
                .Select(g => new { Frequency = g.Count() })
                .OrderByDescending(g => g.Frequency);

            var sb = new StringBuilder();
            foreach (var count in counts)
            {
                sb.Append(count.Frequency);
            }
            return sb.ToString();
        }
    }

    class TreeNodeItem 
    {
        public IList<string> items_to_calculate = new List<string>();
        public IList<string> grouping = new List<string>() ;
    }

    class TreeNode<T>
    {
        protected readonly TreeNode<T> _parent;
        protected readonly T _obj;
        protected readonly IList<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode<T> Parent
        {
            get { return _parent; }
        }
        public T Item {get { return _obj; }}
        public IEnumerable<TreeNode<T>> Children { get { return _children; } }

        public TreeNode(TreeNode<T> parent, T obj)
        {
            _parent = parent;
            _obj = obj;
        }

        public void AddChild(TreeNode<T> node)
        {
            _children.Add(node);
        }


    }
}