using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiAA_4_2
{
    /*
    class Node
    {
        public int value;
        public Node(int val)
        {
            value = val;
        }
    }
    class DSU
    {

        public Node[] elems;
        public DSU(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                elems[i] = new Node(arr[i]);
            }
        }

        public void caclulateCluster()
        {

        }
    }
    */
    class LST
    {
        public Node head = null;
        public int cnt = 1;
        public LST(int data)
        {
            head = new Node(data);
        }

        public void add(int i)
        {
            if (head == null)
                head = new Node(i);
            else
            {
                Node currentNode = head;
                while (currentNode.next != null)
                {
                    currentNode = currentNode.next;
                }
                currentNode.next = new Node(i);
            }

            cnt++;
        }
    }
    class Node
    {
        public Node next = null;
        public int data;
        public Node(int val)
        {
            data = val;
        }
    }
    class UFF
    {
        public int height;
        public int width;
        public int[] mainsElems;
        public LST[] lsts;
        public UFF(int N, int[] arr,int M)
        {
            height = N;
            width = M;
            lsts = new LST[N*M];
            mainsElems = new int[N*M];
            for (int i = 0; i < N*M; i++)
            {
                mainsElems[i] = i;
                lsts[i] = new LST(arr[i]);
            }

            
        }

        public void merge(int x, int y)
        {
            int prX = mainsElems[x], prY = mainsElems[y];
            LST lstFirst = lsts[prX];
            LST lstSecond = lsts[prY];
            Node currentNode = lstFirst.head;
            while (currentNode != null)
            {
                int i = currentNode.data;
                mainsElems[x] = prY;
                lstSecond.add(i);
                currentNode = currentNode.next;
            }
            lsts[prX] = new LST(0);
        }

        public int calculateCluster()
        {
            for (int i = 0; i < lsts.Length; i++)
            {
                if (lsts[i].head.data == 1)
                {
                    int level = i / width;
                    if ((i + 1) / width == level && i+1 < width * height)
                    {
                        if (lsts[i + 1].head.data == 1)
                        {
                            merge(i, i + 1);
                        }
                    }
                    if ((i - 1) / width == level && i - 1 >= 0)
                    {
                        if (lsts[i - 1].head.data == 1)
                        {
                            merge(i, i - 1);
                        }
                    }
                    if (i - width >= 0)
                    {
                        if (lsts[i - width].head.data == 1)
                        {
                            merge(i, i - width);
                        }
                    }

                    if (i + width < width * height)
                    {
                        if (lsts[i + width].head.data == 1)
                        {
                            merge(i, i + width);
                        }
                    }
                }
            }

            int clusterN = 0;
            for (int i = 0; i < lsts.Length; i++)
            {
                if (lsts[i].cnt > 1)
                {
                    clusterN++;
                    continue;
                }

                if (lsts[i].head.data == 1)
                {
                    clusterN++;
                }
            }

            return clusterN;

        }

       

    }
    class Program
    {
        static void Main(string[] args)
        {
            FileStream inFileStream = new FileStream("./" + args[0], FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFileStream);
            FileStream outFileStream = new FileStream("./programm.out", FileMode.Open, FileAccess.Write);
            outFileStream.SetLength(0);
            StreamWriter writer = new StreamWriter(outFileStream);
            string[] inVar = reader.ReadLine().Split(' ');

            string str = "";
            int cnt = 0;
            int N = int.Parse(inVar[0]);
            int M = int.Parse(inVar[1]);
            int[] myArr = new int[N*M];
            for (int i = 0; i < N; i++)
            {
                str = reader.ReadLine();
                for (int j = 0; j < str.Length; j++)
                {

                    myArr[cnt] = int.Parse(str[j]+"");
                    cnt++;
                    

                }
            }

            
            UFF dsu = new UFF(myArr.Length/M, myArr, M);
            int result = dsu.calculateCluster();
            writer.Write(result);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
