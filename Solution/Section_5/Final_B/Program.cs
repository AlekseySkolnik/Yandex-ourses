using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

// Удали узел
namespace Section_5
{
    /*
    Задание
    Дано бинарное дерево поиска BST, в котором хранятся ключи.
    Ключи — уникальные целые числа.
    Найдите вершину с заданным ключом и удалите её из дерева так, чтобы дерево осталось корректным бинарным деревом поиска.
    Если ключа в дереве нет, то изменять дерево не надо.
    На вход вашей функции подаётся корень дерева и ключ, который надо удалить.
    Функция должна вернуть корень изменённого дерева.
    Сложность удаления узла должна составлять O(h), где h –— высота дерева.
    Создавать новые вершины (вдруг очень захочется) нельзя.

   */

    /*
    -- ПРИНЦИП РАБОТЫ --
    1. Ищем в дереве вершину с заданным ключом и ее родителя.
    2. Если ее нет, возвращаем исходное дерево.
    3. Если вершина найдена и она является листом, то исключаем ее из дерева путем удаления ссылки на нее у её родителя.
    4. Если вершина найдена и она не является листом, то ищем вершину P, которая объединит распадающиеся поддеревья, и ее родителя.
    5. Если у вершины P есть потомки, то прародитель parent_P должен усыновить ребенка P.
    6. Удаляем вершину P со старого места. Для этого удалим ссылку на неё из parent_P.
    7. Вершине P присваиваем детей D: левым ребёнком P станет левый ребёнок D (P.left = D.left), а правым — правый (P.right = D.right).
    8. У родителя вершины D меняем ребёнка D на P. В этом месте мы не знаем, была ли вершина D правым или левым ребёнком своего родителя.
       Надо проверить каждого из детей D.parent, чтобы узнать, какого ребёнка заменит вершина P.

    -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

    -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
    O(h), где h - высота дерева. В худшем случае: O(n).

    -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
    O(h), где h - высота дерева
     */

    /// <summary>
    /// ID успешной посылки: https://contest.yandex.ru/contest/24810/run-report/89642767
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// Функция должна вернуть корень изменённого дерева.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Node Remove(Node root, int key)
        {
            // 1. Ищем удаляемую вершину и ее родителя
            var t = FindNode(key, root);
            var parent_D = t.Item1;
            var D = t.Item2;

            // 2. Вершина не найдена, ничего делать не нужно
            if (D == null)
            {
                return root;
            }

            // 3. Вершина найдена и она является листом
            if (D.Left == null && D.Right == null)
            {
                // Если мы удаляем лист, то дерево останется одним деревом и не распадётся на части.
                if (parent_D == null)
                {
                    // Если вершина является корнем.
                    root = null;
                }
                else if (parent_D.Right == D)
                {
                    parent_D.Right = null;
                }
                else
                {
                    parent_D.Left = null;
                }

                return root;
            }

            // 4. Вершина найдена и она НЕ является листом
            // Ищем вершину P, которая объединит распадающиеся поддеревья, и ее родителя.
            var tup = D.Right != null ? FindRightMin(D, D.Right) : FindLeftMax(D, D.Left);
            Node parent_P = tup.Item1;
            Node P = tup.Item2;

            // 5. Если у вершины P есть потомки, то прародитель parent_P должен усыновить ребенка P
            if (!(P != null && P.Left == null && P.Right == null))
            {
                if (parent_P != null && parent_P.Right == P)
                {
                    parent_P.Right = P.Left != null ? P.Left : P.Right;
                }

                if (parent_P != null && parent_P.Left == P)
                {
                    parent_P.Left = P.Right != null ? P.Right : P.Left;
                }
            }

            // 6. Теперь у вершины P нет детей, и её можно удалить.
            // Удаляем вершину P со старого места. Для этого удалим ссылку на неё из parent_P
            if (P == parent_P.Left)
            {
                parent_P.Left = null;
            }
            else
            {
                parent_P.Right = null;
            }

            // 7. Вершине P присваиваем детей D
            if (P != null)
            {
                //Правым ребёнком P станет правый ребёнок D (P.right = D.right).
                if (P.Right == null)
                {
                    P.Right = D.Right;
                }

                //Левым ребёнком P станет левый ребёнок D (P.left = D.left)
                if (P.Left == null)
                {
                    P.Left = D.Left;
                }
            }

            //  8. У родителя вершины D меняем ребёнка D на P.
            // В этом месте мы не знаем, была ли вершина D правым или левым ребёнком своего родителя.
            // Надо проверить каждого из детей D.parent, чтобы узнать, какого ребёнка заменит вершина P.
            if (parent_D == null)
            {
                root = P;
            }
            else if (parent_D.Right == D)
            {
                parent_D.Right = P;
            }
            else
            {
                parent_D.Left = P;
            }

            return root;
        }

        /// <summary>
        /// Поиск удаляемой вершины и ее родителя
        /// </summary>
        /// <param name="key"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        private static Tuple<Node, Node> FindNode(int key, Node root)
        {
            Node parent = null;

            while (root != null)
            {
                if (root.Value == key)
                {
                    return new Tuple<Node, Node>(parent, root);
                }

                parent = root;

                if (root.Right != null && root.Left != null)
                {
                    root = key > root.Value ? root.Right : root.Left;
                }
                else
                {
                    root = root.Left != null ? root.Left : root.Right;
                }
            }

            return new Tuple<Node, Node>(null, null);
        }

        /// <summary>
        /// Находит самую правую вершину в левом поддереве. 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static Tuple<Node, Node> FindRightMin(Node parent, Node node)
        {
            if (node != null && node.Left != null)
            {
                return FindRightMin(node, node.Left);
            }
            else
            {
                return new Tuple<Node, Node>(parent, node);
            }
        }

        /// <summary>
        /// Находит самую левую вершину в правом поддереве.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static Tuple<Node, Node> FindLeftMax(Node parent, Node node)
        {
            if (node != null && node.Right != null)
            {
                return FindLeftMax(node, node.Right);
            }
            else
            {
                return new Tuple<Node, Node>(parent, node);
            }
        }

        [Obsolete("Много по памяти из-за большой глубины рекурсии")]
        private static Tuple<Node, Node> FindNodeRecursive(int value, Node parent, Node child = null)
        {
            if (parent == null)
            {
                return new Tuple<Node, Node>(null, child);
            }

            if (parent.Value == value)
            {
                return new Tuple<Node, Node>(null, parent);
            }

            if (child != null && child.Value == value)
            {
                return new Tuple<Node, Node>(parent, child);
            }

            if (parent.Value > value)
            {
                child = parent.Left;
            }

            if (parent.Value < value)
            {
                child = parent.Right;
            }

            return FindNodeRecursive(value, parent, child);
        }
    }

    public class Final_B
    {
        public static void Main(string[] args)
        {
            var node1 = new Node(2);
            var node2 = new Node(3)
            {
                Left = node1
            };

            var node3 = new Node(1)
            {
                Right = node2
            };

            var node4 = new Node(6);
            var node5 = new Node(8)
            {
                Left = node4
            };

            var node6 = new Node(10)
            {
                Left = node5
            };

            var node7 = new Node(5)
            {
                Left = node3,
                Right = node6
            };

            var newHead = Solution.Remove(node7, 10);

            Console.WriteLine(newHead.Value == 5);
            Console.WriteLine(newHead.Right == node5);
            Console.WriteLine(newHead.Right.Value == 8);
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
}