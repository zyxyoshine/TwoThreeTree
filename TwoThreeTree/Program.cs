using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoThreeTree
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new form());
        }
    }
    public class Node
    {
        public Node parent;
        public Node leftChild;
        public Node rightChild;
        public Node middleChild;

        public int leftVal;
        public int rightVal;

        public bool twoNode;

        public Node()
        {
            twoNode = false;
        }

        public Node(TextBox t1)
        {
            t1.Text = "text";
        }

        public static Node newTwoNode(int value)
        {
            Node node = new Node();
            node.leftVal = value;
            node.twoNode = true;
            return node;

        }

        public static Node newThreeNode(int leftVal, int rightVal)
        {
            Node node = new Node();
            if (leftVal > rightVal)
            {
                node.rightVal = leftVal;
                node.leftVal = rightVal;
            }
            else
            {
                node.leftVal = leftVal;
                node.rightVal = rightVal;
            }
            node.twoNode = false;
            return node;
        }

        public void setLeftChild(Node leftChild)
        {
            this.leftChild = leftChild;
            if (leftChild != null)
                leftChild.setParent(this);
        }

        public void removeChildren()
        {
            this.leftChild = null;
            this.rightChild = null;
        }


        public void setRightChild(Node rightChild)
        {
            this.rightChild = rightChild;
            if (rightChild != null)
                rightChild.setParent(this);
        }

        public void setMiddleChild(Node middleChild)
        {
            if (isThreeNode())
            {
                this.middleChild = middleChild;
                if (middleChild != null)
                {
                    middleChild.setParent(this);
                }
            }
        }

        public void setParent(Node parent)
        {
            this.parent = parent;
        }

        public bool isTerminal()
        {
            return leftChild == null && rightChild == null;
        }


        public int val()
        {
                return leftVal;
        }

        public void setVal(int val)
        {
            if(isTwoNode())
            {
                leftVal = val;
            }
        }



        public void setLeftVal(int leftVal)
        {
            if (isThreeNode())
            {
                this.leftVal = leftVal;
            }
        }

        public void setRightVal(int rightVal)
        {
            if(isThreeNode())
            {
                this.rightVal = rightVal;
            }
        }

        public bool isTwoNode()
        {
            return twoNode;
        }

        public bool isThreeNode()
        {
            return !isTwoNode();
        }

        public void replaceChild(Node currentChild, Node newChild)
        {
            if (currentChild == leftChild)
            {
                leftChild = newChild;
            }
            else if (currentChild == rightChild)
            {
                rightChild = newChild;
            }
            else
            {
                if(middleChild == currentChild)
                {
                    middleChild = newChild;
                }
            }
            newChild.setParent(this);
            currentChild.setParent(null);
        }

        public static HoleNode newHole()
        {
            return new HoleNode();
        }
    }

    public class HoleNode:Node
    {
        public Node child;

        public HoleNode()
        {
            twoNode = false;
        }
        new public bool isTwoNode()
        {
            return false;
        }

        public Node sibling()
        {
            if (parent != null)
            {
                return parent.leftChild == this ? parent.rightChild : parent.leftChild;
            }
            return null;
        }

        new public void setLeftChild(Node leftChild)
        {
        }

        new public void removeChildren()
        {
            child = null;
        }

        new public void setRightChild(Node rightChild)
        {
        }

        public void setChild(Node child)
        {
            this.child = child;
        }
    }

    public class TwoThreeTree
    {
        public static Node root;
        public static int size = 0;

        public static void showNode(Node node, TextBox tn)
        {
            if (node == null)
                return;
            if (node.isTwoNode())
            {
                tn.Text += "key=" + node.val() + Environment.NewLine;
            }
            if (node.isThreeNode())
            {
                tn.Text += "Lkey=" + node.leftVal + " Rkey=" + node.rightVal + Environment.NewLine;
            }
        }

        public static bool add(int value , TextBox ta)
        {
            ta.Text += "开始插入：" + Environment.NewLine;
            if (root == null)
            {
                ta.Text += "根为空,生成新的大小为 " + value + " 的根" + Environment.NewLine;
                root = Node.newTwoNode(value);
            }
            else
            {
                try
                {
                    ta.Text += "根非空,尝试插入" + value + Environment.NewLine;
                    Node result = insert(value, root, ta);
                    if (result != null)
                    {
                        root = result;
                    }
                }
                catch
                {
                    ta.Text += "插入失败！" + Environment.NewLine;
                    return false;
                }
            }
            size++;
            ta.Text += "插入" + value + "成功 总大小为：" + size + Environment.NewLine;
            return true;
        }

        public static bool contains(int value, TextBox tc)
        {
            return findNode(root, value, tc) != null;
        }


        public static Node findNode(Node node, int value, TextBox tf)
        {
            tf.Text += "查找" + value + Environment.NewLine;
            showNode(node,tf);

            if (node == null) return null;

            if (node.isThreeNode())
            {
                int leftComp = value - node.leftVal;
                int rightComp = value - node.rightVal;
                if (leftComp == 0 || rightComp == 0)
                {
                    tf.Text += "成功找到" + value + Environment.NewLine;
                    return node;
                }
                if (leftComp < 0)
                {
                    tf.Text += "比左key小，往左找" + Environment.NewLine;
                    return findNode(node.leftChild, value,tf);
                }
                else if (rightComp < 0)
                {
                    tf.Text += "比左key大，比右key小，往中找" + Environment.NewLine;
                    return findNode(node.middleChild, value,tf);
                }
                else
                {
                    tf.Text += "比右key小，往右找" + Environment.NewLine;
                    return findNode(node.rightChild, value,tf);
                }
            }
            else
            {
                int comp = value - node.val();
                if (comp == 0)
                {
                    tf.Text += "成功找到" + value + Environment.NewLine;
                    return node;
                }
                if (comp < 0)
                {
                    tf.Text += "比key小，往左找" + Environment.NewLine;
                    return findNode(node.leftChild, value, tf);
                }
                else
                {
                    tf.Text += "比key大，往右找" + Environment.NewLine;
                    return findNode(node.rightChild, value, tf);
                }
            }
        }

        public static Node insert(int value, Node node ,TextBox ti)
        {
            Node returnValue = null;
            showNode(node, ti);
            ti.Text += "当前尝试插入" + value + Environment.NewLine;
            if (node.isTwoNode())
            {
                ti.Text += "所在结点是2结点" + Environment.NewLine;

                int comp = value - node.val();
                if (comp == 0)
                {
                    ti.Text += "已经存在值" + value + "无需处理" + Environment.NewLine;
                    size--;
                    return returnValue;
                }
                if (node.isTerminal())
                {
                    ti.Text += "所在结点是叶子" + Environment.NewLine;
                    if (comp == 0)
                        return returnValue;
                    ti.Text += "直接插入" + Environment.NewLine;
                    Node thnode = Node.newThreeNode(value, node.val());
                    Node parent = node.parent;
                    if (parent != null)
                        parent.replaceChild(node, thnode);
                    else
                        root = thnode;
                }
                else
                {
                    ti.Text += "所在结点不是叶子" + Environment.NewLine;
                    if (comp < 0)
                    {
                        ti.Text += "待插入的值比当前结点小,开始往左儿子处理" + Environment.NewLine;
                        Node result = insert(value, node.leftChild,ti);
                        if (result != null)
                        {
                            Node threeNode = Node.newThreeNode(result.val(), node.val());
                            threeNode.setRightChild(node.rightChild);
                            threeNode.setMiddleChild(result.rightChild);
                            threeNode.setLeftChild(result.leftChild);
                            if (node.parent != null)
                            {
                                node.parent.replaceChild(node, threeNode);
                            }
                            else
                            {
                                root = threeNode;
                            }
                            unlinkNode(node);
                        }
                    }
                    else if (comp > 0)
                    {
                        ti.Text += "待插入的值比当前结点大,开始往右儿子处理" + Environment.NewLine;
                        Node result = insert(value, node.rightChild,ti);
                        if (result != null)
                        {
                            Node threeNode = Node.newThreeNode(result.val(), node.val());
                            threeNode.setLeftChild(node.leftChild);
                            threeNode.setMiddleChild(result.leftChild);
                            threeNode.setRightChild(result.rightChild);
                            if (node.parent != null)
                            {
                                node.parent.replaceChild(node, threeNode);
                            }
                            else
                            {
                                root = threeNode;
                            }
                            unlinkNode(node);
                        }
                    }
                    else
                    {
                        return returnValue;
                    }
                }

            } else { // 3 node
                ti.Text += "所在结点是3结点" + Environment.NewLine;

                Node threeNode = node;

                int leftComp = value - threeNode.leftVal;
                int rightComp = value - threeNode.rightVal;
                if (leftComp == 0 || rightComp == 0)
                {
                    ti.Text += value + "与当前结点左或右key相等，无需处理" + Environment.NewLine;
                    size--;
                    return returnValue;
                }

                if (threeNode.isTerminal())
                {
                    ti.Text += value + "所在结点是叶子，尝试分裂" + Environment.NewLine;
                    returnValue = splitNode(threeNode, value ,ti);
                }
                else
                {
                    if (leftComp < 0)
                    {
                        ti.Text += value + "比当前结点左key小，往左儿子处理" + Environment.NewLine;
                        Node result = insert(value, threeNode.leftChild, ti);
                        if (result != null)
                        {
                            returnValue = splitNode(threeNode, result.val(), ti);
                            returnValue.leftChild.setLeftChild(result.leftChild);
                            returnValue.leftChild.setRightChild(result.rightChild);
                            returnValue.rightChild.setLeftChild(threeNode.middleChild);
                            returnValue.rightChild.setRightChild(threeNode.rightChild);
                            unlinkNode(threeNode);
                        }
                    }
                    else if (rightComp < 0)
                    {
                        ti.Text += value + "比当前结点左key大，比右key小，往中儿子处理" + Environment.NewLine;
                        Node result = insert(value, threeNode.middleChild, ti);
                        if (result != null)
                        {
                            returnValue = splitNode(threeNode, result.val(), ti);
                            returnValue.leftChild.setLeftChild(threeNode.leftChild);
                            returnValue.leftChild.setRightChild(result.leftChild);
                            returnValue.rightChild.setLeftChild(result.rightChild);
                            returnValue.rightChild.setRightChild(threeNode.rightChild);
                            unlinkNode(threeNode);
                        }
                    }
                    else
                    {
                        ti.Text += value + "比当前结点右key大，往右儿子处理" + Environment.NewLine;
                        Node result = insert(value, threeNode.rightChild ,ti);
                        if (result != null)
                        {
                            returnValue = splitNode(threeNode, result.val(), ti);
                            returnValue.leftChild.setLeftChild(threeNode.leftChild);
                            returnValue.leftChild.setRightChild(threeNode.middleChild);
                            returnValue.rightChild.setLeftChild(result.leftChild);
                            returnValue.rightChild.setRightChild(result.rightChild);
                            unlinkNode(threeNode);
                        }
                    }
                }
            }
            return returnValue;
        }

        public static void unlinkNode(Node node)
        {
            node.removeChildren();
            node.setParent(null);
        }

        public static Node splitNode(Node threeNode, int value, TextBox ts)
        {
            ts.Text += "开始分裂" + Environment.NewLine;
            showNode(threeNode, ts);
            int min;
            int max;
            int middle;
            if (value - threeNode.leftVal < 0)
            {
                min = value;
                middle = threeNode.leftVal;
                max = threeNode.rightVal;
                ts.Text += value + "比左key小" + ",min=" + min + " middle=" + middle + " max=" + max + Environment.NewLine;
            }
            else if (value - threeNode.rightVal < 0)
            {
                min = threeNode.leftVal;
                middle = value;
                max = threeNode.rightVal;
                ts.Text += value + "比左key大，比右key小" + ",min=" + min + " middle=" + middle + " max=" + max + Environment.NewLine;
            }
            else
            {
                min = threeNode.leftVal;
                max = value;
                middle = threeNode.rightVal;
                ts.Text += value + "比右key大" + ",min=" + min + " middle=" + middle + " max=" + max + Environment.NewLine;
            }
            ts.Text += middle + "作为父亲，" + min + "为左儿子，" + max + "为右儿子" + Environment.NewLine;
            Node parent = Node.newTwoNode(middle);
            parent.setLeftChild(Node.newTwoNode(min));
            parent.setRightChild(Node.newTwoNode(max));
            return parent;
        }

        public static bool remove(int value, TextBox tr)
        {
            if (size < 1)
            {
                tr.Text += "不存在结点,删除失败！" + Environment.NewLine;
                return false;
            }
            tr.Text += "开始尝试删除" + value + Environment.NewLine;
            Node node = findNode(root, value, tr);
            if (node == null)
            {
                tr.Text += "找不到" + value + "结点,删除失败！"+ Environment.NewLine;
                return false;
            }
            showNode(node, tr);
            HoleNode hole = null;
            Node terminalNode;
            int holeValue;
            if (node.isTerminal())
            {
                tr.Text += "结点为叶子结点,直接处理" + Environment.NewLine;
                terminalNode = node;
                holeValue = value;
            }
            else
            {
                tr.Text += "结点不是叶子结点，根据遍历序处理" + Environment.NewLine;
                if (node.isThreeNode())
                {
                    tr.Text += "结点是3结点" + Environment.NewLine;
                    if (node.leftVal == value)
                    {
                        tr.Text += "左key等于" + value + "，叶子为前驱" + Environment.NewLine;
                        Node pred = predecessor(node, value);
                        holeValue = pred.isThreeNode() ? pred.rightVal : pred.val();
                        node.setLeftVal(holeValue);
                        terminalNode = pred;
                    }
                    else
                    {
                        tr.Text += "左key不等于" + value + "，叶子为后继" + Environment.NewLine;
                        Node succ = successor(node, value);
                        holeValue = succ.isThreeNode() ? succ.leftVal : succ.val();
                        node.setRightVal(holeValue);
                        terminalNode = succ;
                    }
                }
                else
                {
                    tr.Text += "结点是2结点，叶子为后继" + Environment.NewLine;
                    Node succ = successor(node, value);
                    holeValue = succ.isThreeNode() ? succ.leftVal : succ.val();
                    node.setVal(holeValue);
                    terminalNode = succ;
                }
            }

            if (!terminalNode.isTerminal())
            {
                tr.Text += "删除出错！" + Environment.NewLine;
                return false;
            }
            showNode(terminalNode, tr);
            if (terminalNode.isThreeNode())
            {
                tr.Text += "叶子结点是3结点，变换成2结点" + Environment.NewLine;
                int val = terminalNode.leftVal == holeValue ? terminalNode.rightVal : terminalNode.leftVal;
                Node twoNode = Node.newTwoNode(val);
                if (terminalNode.parent != null)
                {
                    terminalNode.parent.replaceChild(terminalNode, twoNode);
                }
                else
                {
                    root = twoNode;
                }
            }
            else
            {
                tr.Text += "叶子结点是2结点，变换成空结点" + Environment.NewLine;
                if (terminalNode.parent != null)
                {
                    hole = Node.newHole();
                    terminalNode.parent.replaceChild(terminalNode, hole);
                }
                else
                {
                    root = null;
                }
            }
            tr.Text += "根据当前结点的不同情形处理" + Environment.NewLine;
            while (hole != null)
            {
                showNode(hole,tr);
                if (hole.parent.isTwoNode() && hole.sibling().isTwoNode())
                {
                    tr.Text += "当前结点的父亲为2结点，兄弟为2结点" + Environment.NewLine;
                    Node parent = hole.parent;
                    Node sibling = hole.sibling();

                    Node threeNode = Node.newThreeNode(parent.val(), sibling.val());
                    if (parent.leftChild == hole)
                    {
                        threeNode.setLeftChild(hole.child);
                        threeNode.setMiddleChild(sibling.leftChild);
                        threeNode.setRightChild(sibling.rightChild);
                    }
                    else
                    {
                        threeNode.setLeftChild(sibling.leftChild);
                        threeNode.setMiddleChild(sibling.rightChild);
                        threeNode.setRightChild(hole.child);
                    }

                    if (parent.parent == null)
                    {
                        unlinkNode(hole);
                        root = threeNode;
                        hole = null;
                    }
                    else
                    {
                        hole.setChild(threeNode);
                        parent.parent.replaceChild(parent, hole);
                    }
                    unlinkNode(parent);
                    unlinkNode(sibling);

                }
                else if (hole.parent.isTwoNode() && hole.sibling().isThreeNode())
                {
                    tr.Text += "当前结点的父亲为2结点，兄弟为3结点" + Environment.NewLine;
                    Node parent = hole.parent;
                    Node sibling = hole.sibling();

                    if (parent.leftChild == hole)
                    {
                        Node leftChild = Node.newTwoNode(parent.val());
                        Node rightChild = Node.newTwoNode(sibling.rightVal);
                        parent.setVal(sibling.leftVal);
                        parent.replaceChild(hole, leftChild);
                        parent.replaceChild(sibling, rightChild);
                        leftChild.setLeftChild(hole.child);
                        leftChild.setRightChild(sibling.leftChild);
                        rightChild.setLeftChild(sibling.middleChild);
                        rightChild.setRightChild(sibling.rightChild);
                    }
                    else
                    {
                        Node leftChild = Node.newTwoNode(sibling.leftVal);
                        Node rightChild = Node.newTwoNode(parent.val());
                        parent.setVal(sibling.rightVal);
                        parent.replaceChild(sibling, leftChild);
                        parent.replaceChild(hole, rightChild);
                        leftChild.setLeftChild(sibling.leftChild);
                        leftChild.setRightChild(sibling.middleChild);
                        rightChild.setLeftChild(sibling.rightChild);
                        rightChild.setRightChild(hole.child);
                    }
                    unlinkNode(hole);
                    unlinkNode(sibling);
                    hole = null;
                }

                else if (hole.parent.isThreeNode())
                {
                    tr.Text += "当前结点的父亲为3结点，";
                    Node parent = hole.parent;

                    if (parent.middleChild == hole && parent.leftChild.isTwoNode())
                    {
                        tr.Text += "且为父亲的middle,左兄弟为2结点" + Environment.NewLine;
                        Node leftChild = parent.leftChild;
                        Node newParent = Node.newTwoNode(parent.rightVal);
                        Node newLeftChild = Node.newThreeNode(leftChild.val(), parent.leftVal);
                        newParent.setLeftChild(newLeftChild);
                        newParent.setRightChild(parent.rightChild);
                        if (parent != root)
                        {
                            parent.parent.replaceChild(parent, newParent);
                        }
                        else
                        {
                            root = newParent;
                        }

                        newLeftChild.setLeftChild(leftChild.leftChild);
                        newLeftChild.setMiddleChild(leftChild.rightChild);
                        newLeftChild.setRightChild(hole.child);

                        unlinkNode(parent);
                        unlinkNode(leftChild);
                        unlinkNode(hole);
                        hole = null;
                    }
                    else if (parent.middleChild == hole && parent.rightChild.isTwoNode())
                    {
                        tr.Text += "且为父亲的middle,右兄弟为2结点" + Environment.NewLine;
                        Node rightChild = parent.rightChild;
                        Node newParent = Node.newTwoNode(parent.leftVal);
                        Node newRightChild = Node.newThreeNode(parent.rightVal, rightChild.val());
                        newParent.setLeftChild(parent.leftChild);
                        newParent.setRightChild(newRightChild);
                        if (parent != root)
                        {
                            parent.parent.replaceChild(parent, newParent);
                        }
                        else
                        {
                            root = newParent;
                        }
                        newRightChild.setLeftChild(hole.child);
                        newRightChild.setMiddleChild(rightChild.leftChild);
                        newRightChild.setRightChild(rightChild.rightChild);
                        unlinkNode(parent);
                        unlinkNode(rightChild);
                        unlinkNode(hole);
                        hole = null;
                    }
                    else if (parent.middleChild.isTwoNode())
                    {
                        Node middleChild = parent.middleChild;
                        if (parent.leftChild == hole)
                        {
                            tr.Text += "且为父亲的left,middle兄弟为2结点" + Environment.NewLine;
                            Node newParent = Node.newTwoNode(parent.rightVal);
                            Node leftChild = Node.newThreeNode(parent.leftVal, middleChild.val());
                            newParent.setLeftChild(leftChild);
                            newParent.setRightChild(parent.rightChild);
                            if (parent != root)
                            {
                                parent.parent.replaceChild(parent, newParent);
                            }
                            else
                            {
                                root = newParent;
                            }

                            leftChild.setLeftChild(hole.child);
                            leftChild.setMiddleChild(middleChild.leftChild);
                            leftChild.setRightChild(middleChild.rightChild);

                            unlinkNode(parent);
                            unlinkNode(hole);
                            unlinkNode(middleChild);
                            hole = null;
                        }
                        else if (parent.rightChild == hole)
                        {
                            tr.Text += "且为父亲的right,middle兄弟为2结点" + Environment.NewLine;
                            Node newParent = Node.newTwoNode(parent.leftVal);
                            Node rightChild = Node.newThreeNode(middleChild.val(), parent.rightVal);
                            newParent.setRightChild(rightChild);
                            newParent.setLeftChild(parent.leftChild);
                            if (parent != root)
                            {
                                parent.parent.replaceChild(parent, newParent);
                            }
                            else
                            {
                                root = newParent;
                            }

                            rightChild.setLeftChild(middleChild.leftChild);
                            rightChild.setMiddleChild(middleChild.rightChild);
                            rightChild.setRightChild(hole.child);

                            unlinkNode(parent);
                            unlinkNode(hole);
                            unlinkNode(middleChild);
                            hole = null;
                        }
                    }

                    else if (parent.middleChild.isThreeNode())
                    {
                        tr.Text += "当前结点的父亲为3结点，兄弟为3结点,";
                        Node middleChild = parent.middleChild;
                        if (hole == parent.leftChild)
                        {
                            tr.Text += "且为父亲的左儿子" + Environment.NewLine; 
                            Node newLeftChild = Node.newTwoNode(parent.leftVal);
                            Node newMiddleChild = Node.newTwoNode(middleChild.rightVal);
                            parent.setLeftVal(middleChild.leftVal);
                            parent.setLeftChild(newLeftChild);
                            parent.setMiddleChild(newMiddleChild);
                            newLeftChild.setLeftChild(hole.child);
                            newLeftChild.setRightChild(middleChild.leftChild);
                            newMiddleChild.setLeftChild(middleChild.middleChild);
                            newMiddleChild.setRightChild(middleChild.rightChild);

                            unlinkNode(hole);
                            unlinkNode(middleChild);
                            hole = null;
                        }
                        else if (hole == parent.rightChild)
                        {
                            tr.Text += "且为父亲的右儿子" + Environment.NewLine;
                            Node newMiddleChild = Node.newTwoNode(middleChild.leftVal);
                            Node newRightChild = Node.newTwoNode(parent.rightVal);
                            parent.setRightVal(middleChild.rightVal);
                            parent.setMiddleChild(newMiddleChild);
                            parent.setRightChild(newRightChild);
                            newMiddleChild.setLeftChild(middleChild.leftChild);
                            newMiddleChild.setRightChild(middleChild.middleChild);
                            newRightChild.setLeftChild(middleChild.rightChild );
                            newRightChild.setRightChild(hole.child);

                            unlinkNode(hole);
                            unlinkNode(middleChild);
                            hole = null;

                        }
                        else if (hole == parent.middleChild && parent.leftChild.isThreeNode())
                        {
                            tr.Text += "且为父亲的中儿子，左兄弟为3儿子" + Environment.NewLine;
                            Node leftChild = parent.leftChild;
                            Node newLeftChild = Node.newTwoNode(leftChild.leftVal);
                            Node newMiddleChild = Node.newTwoNode(parent.leftVal);
                            parent.setLeftVal(leftChild.rightVal);
                            parent.setLeftChild(newLeftChild);
                            parent.setMiddleChild(newMiddleChild);
                            newLeftChild.setLeftChild(leftChild.leftChild);
                            newLeftChild.setRightChild(leftChild.middleChild);
                            newMiddleChild.setLeftChild(leftChild.rightChild);
                            newMiddleChild.setRightChild(hole.child);

                            unlinkNode(hole);
                            unlinkNode(leftChild);
                            hole = null;
                        }
                        else
                        {
                            if (!(hole == parent.middleChild && parent.rightChild.isThreeNode()))
                            {
                                return false;
                            }
                            tr.Text += "且为父亲的中儿子，右兄弟为3儿子" + Environment.NewLine;
                            Node rightChild = parent.rightChild;
                            Node newRightChild = Node.newTwoNode(rightChild.rightVal);
                            Node newMiddleChild = Node.newTwoNode(parent.rightVal);
                            parent.setRightVal(rightChild.leftVal);
                            parent.setMiddleChild(newMiddleChild);
                            parent.setRightChild(newRightChild);
                            newRightChild.setRightChild(rightChild.rightChild);
                            newRightChild.setLeftChild(rightChild.middleChild);
                            newMiddleChild.setRightChild(rightChild.leftChild);
                            newMiddleChild.setLeftChild(hole.child);

                            unlinkNode(hole);
                            unlinkNode(rightChild);
                            hole = null;
                        }
                    }

                }
            }
            size--;
            tr.Text += "删除 " + value + " 完成，" + "剩余 " + size + " 个结点" + Environment.NewLine;
            return true;
        }

        public static Node successor(Node node, int value)
        {
            if (node == null)
                return null;

            if (!node.isTerminal())
            {
                Node p;
                if (node.isThreeNode() && node.leftVal == value)
                {
                    p = node.middleChild;
                }
                else
                {
                    p = node.rightChild;
                }
                while (p.leftChild != null)
                {
                    p = p.leftChild;
                }
                return p;
            }
            else
            {
                Node p = node.parent;
                if (p == null) return null;

                Node ch = node;
                while (p != null && ch == p.rightChild)
                {
                    ch = p;
                    p = p.parent;
                }
                return p != null ? p : null;
            }
        }

        public static Node predecessor(Node node, int value)
        {
            if (node == null)
                return null;

            Node p;
            if (!node.isTerminal())
            {
                if (node.isThreeNode() && node.rightVal == value)
                {
                    p = node.middleChild;
                }
                else
                {
                    p = node.leftChild;
                }

                while (p.rightChild != null)
                {
                    p = p.rightChild;
                }
                return p;
            }else
            {
                return null;
            }

        }

    }
}
