using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_player
{
    class img_deal
    {
        /*********************************************************************/
        /// <summary>
        /// 宏定义
        /// </summary>
        /// 
        byte L_BlackEnd = 1;
        byte R_BlackEnd = 2;
        byte M_BlackEnd = 0;
        byte Angle = 3;
        const int OV7725_EAGLE_H = 60, OV7725_EAGLE_W = 80, OV7725_EAGLE_M = 39;

        byte All_Black = 1;
        byte All_White = 2;
        byte None = 0;
        /***********************************************************************/
        public int[] imgbuff = new int[600];      //定义存储接收图像的数组
        int[] img = new int[4800];     //解压后数组
        public int[,] IMG_BUFF = new int[60, 80];
        bool Iscircle = false;
        public bool breakflag = false;
        public bool bturnimg = false;
        public void Turnimage(int[,] img)
        {
            int t = 0;
            for (int i = 0; i < 60; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    t = img[j, i];
                    img[j, i] = img[79 - j, i];
                    img[79 - j, i] = t;
                }
            }
        }
        public void Turnimage2(int[,] img)
        {
            int t = 0;
            for (int i = 0; i < 60; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    t = img[i, j];
                    img[i, j] = img[i, 79 - j];
                    img[i, 79 - j] = t;
                }
            }
        }
        public void image_deal()
        {
            if (breakflag)
            {
                ;
            }
            //camera_get_img();                       //获取图像
            img_extract(img, imgbuff, 600); //解压图像											
            int i, j, k = 0;
            for (i = 0; i < OV7725_EAGLE_H; i++)  //图像转成二维
            {
                for (j = 0; j < OV7725_EAGLE_W; j++)
                {
                    IMG_BUFF[i, j] = img[k];
                    k++;
                }
            }
            if (bturnimg) Turnimage2(IMG_BUFF);
            GetImageParam();//提取图像特征
            MidLineProcess();//中心线处理
            //RTRecognition();//赛道检测
            DirectionCtrol();
        }
        void img_extract(int[] dst, int[] src, int srclen)
        {
            int[] colour = { 255, 0 }; //0 和 1 分别对应的颜色
            int[] mdst = dst;
            int[] msrc = src;
            //注：山外的摄像头 0 表示 白色，1表示 黑色
            int tmpsrc;
            int n1 = 0, n2 = 0;
            while ((srclen--) != 0)
            {
                tmpsrc = msrc[n1++];
                mdst[n2++] = colour[(tmpsrc >> 7) & 0x01];
                mdst[n2++] = colour[(tmpsrc >> 6) & 0x01];
                mdst[n2++] = colour[(tmpsrc >> 5) & 0x01];
                mdst[n2++] = colour[(tmpsrc >> 4) & 0x01];
                mdst[n2++] = colour[(tmpsrc >> 3) & 0x01];
                mdst[n2++] = colour[(tmpsrc >> 2) & 0x01];
                mdst[n2++] = colour[(tmpsrc >> 1) & 0x01];
                mdst[n2++] = colour[(tmpsrc >> 0) & 0x01];
            }
        }
        void GetImageParam()
        {
            GetBlackEndParam();//获取黑线截至行
            //GetExcursionLine();//获取偏移量线，识别十字交叉
            //GetEPerCount();//有效偏移量，平均每列偏移量	
        }
        /******************************************************************************
        //获取黑线截至行
        ******************************************************************************/
        byte BlackEndL = 0;
        byte BlackEndM = 0;
        byte BlackEndR = 0;
        byte BlackEndMax = 0;
        int BlackEndLMR = 0;
        byte g_Derict = 0;
        bool LEndFlag = false;//左截至标志
        bool MEndFlag = false;//中·截至标志
        bool REndFlag = false;//右截至标志
        byte White = 0xff;
        byte Black = 0x00;
        // bool MEndFlag = false;
        byte MAX(byte x, byte y)
        {
            return x > y ? x : y;
        }
        int MIN(int x, int y)
        {
            return x < y ? x : y;
        }
        int ABS(int x)
        {
            return x > 0 ? x : -x;
        }
        public string dir;
        void GetBlackEndParam()//获取黑线截至行
        {
            LEndFlag = false;//左截至标志
            MEndFlag = false;//中·截至标志
            REndFlag = false;//右截至标志
            BlackEndL = 0;//左截至计数
            BlackEndM = 0;//中截至计数
            BlackEndR = 0;//右截至计数

            int i = 0;

            for (i = OV7725_EAGLE_H - 1; i >= 0; i--)
            {
                if (IMG_BUFF[i, OV7725_EAGLE_W / 2 - 1] == White && !MEndFlag)
                {
                    BlackEndM++;//中截至计数增加
                }
                else if (i > 1 && IMG_BUFF[i - 1, OV7725_EAGLE_W / 2 - 1] == Black && IMG_BUFF[i - 2, OV7725_EAGLE_W / 2 - 1] == Black)
                {
                    MEndFlag = true;//上两行为黑，截至标志置位
                }

                if (IMG_BUFF[i, OV7725_EAGLE_W / 4 - 1] == White && !LEndFlag)
                {
                    BlackEndL++;//左截至计数增加
                }
                else if (i > 1 && IMG_BUFF[i - 1, OV7725_EAGLE_W / 4 - 1] == Black && IMG_BUFF[i - 2, OV7725_EAGLE_W / 4 - 1] == Black)
                {
                    LEndFlag = true;//上两行为黑，截至标志置位
                }

                if (IMG_BUFF[i, OV7725_EAGLE_W * 3 / 4 - 1] == White && !REndFlag)
                {
                    BlackEndR++;//右截至计数增加
                }
                else if (i > 1 && IMG_BUFF[i - 1, OV7725_EAGLE_W * 3 / 4 - 1] == Black && IMG_BUFF[i - 2, OV7725_EAGLE_W * 3 / 4 - 1] == Black)
                {
                    REndFlag = true;//上两行为黑，截至标志置位
                }
            }

            //计算最远有效行
            BlackEndMax = MAX(BlackEndL, BlackEndM);
            BlackEndMax = MAX(BlackEndMax, BlackEndR);

            BlackEndLMR = (BlackEndL + BlackEndM + BlackEndR);//最远有效黑点累和
                                                              //判断十字左右倾
            Iscircle = false;
            if (BlackEndMax == BlackEndL)
            {
                if (BlackEndR > BlackEndM && BlackEndL - BlackEndM < 10 && BlackEndL > 30 && BlackEndR > 30 && BlackEndM > 30)
                {
                    Iscircle = true;
                    dir = "圆环";
                    g_Derict = L_BlackEnd;
                }
                else
                {
                    g_Derict = L_BlackEnd;//左
                    dir = "左";
                }
            }
            else if (BlackEndMax == BlackEndR)
            {
                if (BlackEndL > BlackEndM && BlackEndL - BlackEndM < 10 && BlackEndL > 30 && BlackEndR > 30 && BlackEndM > 30)
                {
                    dir = "圆环";
                    g_Derict = R_BlackEnd;
                    Iscircle = true;
                }
                else
                {
                    g_Derict = R_BlackEnd;//右
                    dir = "右";
                }
            }
            else if (BlackEndMax == BlackEndM)
            {
                //if (ABS(BlackEndM - BlackEndR) <= 2 && ABS(BlackEndM - BlackEndL) <= 2)
                //{
                //    g_Derict = Angle;//直角
                //}
                if (ABS(BlackEndL - BlackEndR) < 5)          ///////////////////////需要调（2017年3月22日19:26:13）  
                {
                    g_Derict = M_BlackEnd;//十字
                    dir = "十字";
                }
                else if (BlackEndL > BlackEndR)
                {
                    g_Derict = L_BlackEnd;//左
                    dir = "左";
                }
                else
                {
                    g_Derict = R_BlackEnd;//右
                    dir = "右";
                }
            }
        }

        /******************************************************************************
        获取偏移量线
        ******************************************************************************/
        public int[] LeftBlack = new int[OV7725_EAGLE_H];
        public int[] RightBlack = new int[OV7725_EAGLE_H];
        public int[] BlackLineData = new int[OV7725_EAGLE_H];

        byte FirstImage = 0;
        byte BlackRow = 0;
        byte WhiteRow = 0;
        byte[] LineType = new byte[OV7725_EAGLE_H];//全黑行计数

        //2017年3月27日19:18:58理解：边缘跳变检测（赛道类型用）
        void GetExcursionLine()
        {
            int i = 0, j = 0;
            int CountBlack = 0;
            int CountWhite = 0;
            //指针
            int pLeft, pRight;
            BlackRow = 0;
            WhiteRow = 0;


            //第一幅图像使用
            if (FirstImage == 0)
            {
                for (i = 0; i < OV7725_EAGLE_H; i++)
                {
                    BlackLineData[i] = OV7725_EAGLE_M;
                }
                FirstImage = 1;//标志位置1，第二幅图像及以后不再使用
            }
            //if结束

            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                //指针初始化，从中间
                pLeft = BlackLineData[i];//左边线指针
                pRight = BlackLineData[i];//右边线指针

                LeftBlack[i] = pLeft;//初始化黑边
                RightBlack[i] = pRight;//初始化黑边

                CountBlack = 0;//每行黑点计数
                CountWhite = 0;//每行白点计数

                //两边黑线提取
                for (j = BlackLineData[i]; j >= 0; j--)  //////////////////////////////////
                {
                    //中->左搜索
                    if (pLeft - 5 > 0 && pLeft < 80)
                        if (LeftBlack[i] == BlackLineData[i])
                        {

                            if (IMG_BUFF[i, pLeft] == Black)
                            {
                                CountBlack++;//黑点计数
                            }
                            else
                            {
                                CountWhite++;//白点计数
                            }
                            // 未找到左边缘则寻找
                            if (IMG_BUFF[i, pLeft] != IMG_BUFF[i, pLeft - 3])//检测到边缘
                            {
                                //单线
                                if (ABS(OV7725_EAGLE_M - pLeft) <= 3)
                                {
                                    //找到左边缘
                                    LeftBlack[i] = pLeft - 2;
                                }
                                //确认检测到边缘
                                else if (IMG_BUFF[i, pLeft - 1] != IMG_BUFF[i, pLeft - 4] && IMG_BUFF[i, pLeft - 2] != IMG_BUFF[i, pLeft - 5])
                                {
                                    //找到左边缘
                                    LeftBlack[i] = pLeft - 2;
                                    //右边缘也找到
                                    if (RightBlack[i] != OV7725_EAGLE_W - 1)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    pLeft--;
                                }
                            }
                            else
                            {
                                pLeft--;
                            }
                        }// if结束--′从左到右搜索到边缘

                    //′右->左搜索
                    if (pRight + 5 < 80)
                        if (RightBlack[i] == BlackLineData[i])
                        {

                            if (IMG_BUFF[i, pRight] == Black)
                            {
                                CountBlack++;//黑点计数
                            }
                            else
                            {
                                CountWhite++;//白点计数
                            }

                            if (IMG_BUFF[i, pRight] != IMG_BUFF[i, pRight + 3])
                            {
                                //单线
                                if (ABS(OV7725_EAGLE_M - pRight) <= 3)
                                {
                                    //找到右边缘
                                    RightBlack[i] = pRight + 2;
                                }

                                //确认检测到边缘
                                if (IMG_BUFF[i, pRight + 1] != IMG_BUFF[i, pRight + 4] && IMG_BUFF[i, pRight + 2] != IMG_BUFF[i, pRight + 5])
                                {
                                    //找到右边缘
                                    RightBlack[i] = pRight + 2;
                                    //左边缘也找到
                                    if (LeftBlack[i] != 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    pRight++;
                                }
                            }
                            else
                            {
                                pRight++;
                            }
                        }// if结束--′从左到右搜索到边缘

                    // 无边缘则跳出
                    if (pLeft >= pRight)
                    {
                        LeftBlack[i] = RightBlack[i] = pLeft;
                        //判断记录全黑行

                        if (CountBlack >= (OV7725_EAGLE_W - 5))
                        {
                            CountBlack = 0;
                            LineType[i] = All_Black;//全黑行标志置位
                            BlackRow++;//全黑行计数
                        }

                        else if (CountWhite >= (OV7725_EAGLE_W - 5))
                        {
                            CountWhite = 0;
                            if (i < OV7725_EAGLE_H - 5 && i > 10)//近10行不检测全白行，除去前端盲区
                            {
                                WhiteRow++;//全白行计数
                                LineType[i] = All_White;//全白行标志置位
                            }
                            else
                            {
                                LineType[i] = None;
                            }
                        }
                        else
                        {
                            LineType[i] = None;
                        }
                        break;
                    }
                }//for结束--黑线提取结束
            }//for结束-- 行扫描完

        }


        /******************************************************************************
        有效偏移量，平均每列偏移量
        ******************************************************************************/
        float EPerCount = 0.0f;
        int Excursion = 0;//偏移量
        int ValidLineCount = 0;//有效行计数
        int ValidExcursionCount = 0;//有效偏移量计数
        int[] TripPointPos = new int[OV7725_EAGLE_H];//产生跳变的具体行数写入该数组
        int[] SubValue = new int[OV7725_EAGLE_H];//上下两行产生的跳变差
        //2017年3月27日19:19:33理解：赛道类型判断，
        //函数内参数引用：RTRecognition
        void GetEPerCount()
        {
            int i = 0, j = 0;
            int TripPointCount = 0;//中心跳变计数
            int FilterNumber = 5;// 连续跳变点小于该数则滤掉
            int TripLen = 5;//跳变长度

            Excursion = 0;//偏移量
            ValidLineCount = 0;//有效行计数
            ValidExcursionCount = 0;//有效偏移量计数

            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                TripPointPos[i] = 0;//产生跳变的具体行数写入该数组	
            }
            //跳变分段
            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                if (ABS(RightBlack[i] - LeftBlack[i]) < 3)//g
                {
                    BlackLineData[i] = LeftBlack[i];// 单边缘直接取边缘
                }
                else
                {
                    BlackLineData[i] = LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2;//正常两边黑线提取中心线
                }
                //相邻中心点做差，找出中心跳变处，经行分段
                if (i > 0)
                {
                    SubValue[i] = BlackLineData[i] - BlackLineData[i - 1];//上下两行跳变差
                                                                          //跳变判断
                    if (ABS(SubValue[i]) > TripLen)
                    {
                        //记录跳变位置
                        TripPointPos[TripPointCount] = i;//记录具体行数
                        TripPointCount++;//跳变行数增加
                    }
                }
            }//for结束

            if (TripPointCount == 0)//没有跳变
            {
                for (i = 0; i < OV7725_EAGLE_H; i++)
                {
                    ValidLineCount++;//有效行计数
                    if (i > 25 && i < OV7725_EAGLE_H)//图像下半部//g
                    {
                        ValidExcursionCount++;//有效偏移量增加
                        Excursion += (BlackLineData[i - 1] - BlackLineData[i]);//上下两行相对偏移量累计
                    }
                }
            }
            else //有跳变
            {
                if (TripPointPos[0] > FilterNumber)//第一次跳变时，跳变行数大于8
                {
                    for (i = 0; i < TripPointPos[0]; i++)
                    {
                        ValidLineCount++;//有效行增加
                        if (i > 25)//g
                        {
                            ValidExcursionCount++;//有效偏移量增加
                            Excursion += (BlackLineData[i - 1] - BlackLineData[i]);//上下两行相对偏移量累计
                        }
                    }
                }

                TripPointPos[TripPointCount] = OV7725_EAGLE_H;//最后一次假想跳变位置直接给最大

                for (j = 0; j < TripPointCount; j++)
                {
                    if (TripPointPos[j + 1] - TripPointPos[j] > FilterNumber)//相邻跳变大于5
                    {
                        for (i = TripPointPos[j]; i < TripPointPos[j + 1]; i++)
                        {
                            ValidLineCount++;//有效行增加
                            if (i > TripPointPos[j] && i > 25)
                            {
                                ValidExcursionCount++;//有效偏移量增加
                                Excursion += (BlackLineData[i - 1] - BlackLineData[i]);//偏移量累计
                            }
                        }
                    }//End if
                }//End for
            }//End else
            if (ValidExcursionCount > 1)//有便宜量
            {
                EPerCount = (float)(ABS(Excursion) * 1.0 / ValidExcursionCount);//偏移量平均
            }
            else
            {
                EPerCount = 3;//偏移量平均为3
                Excursion = 50;//总偏移量为50
            }
        }

        const int rout_mid = 40;
        /******************************************************************************
        提取中心线
        ******************************************************************************/
        int NoValidCount = 0;
        int NoValidMax = 0;//连续两边都找不到计数
        int StableNumbers = 0;//稳定行计数
        int StableNumbers2 = 0;//十字稳定行计数
        int LeftStableNumbers = 0;//左稳定行计数
        int RightStableNumbers = 0;//右稳定行计数
        int ValidLineCount1 = 0; //左右都找到
        int ValidLineCount2 = 0;//只要一边找到
        int num = 0;
        public float ke;
        int flag_cross = 0, i_left_old = 0, i_right_old = 0;

        int[] ValidLine = new int[OV7725_EAGLE_H];//有效行计数

        struct site
        {
            public int[] buff_save;
            public int num;
        }
        site left_site, mid_site, right_site;
        //left_site = { { 0 },0 }, mid_site = { { 0 },0 }, right_site = { { 0 },0 };





        /***********************************************

        name:                      black_deal()
        function:                  deal the condition
        when only oneline
        parameter:                 mid:line
        imgb:picture
        return code:               none

        ***********************************************/
        //函数引用：GetLMR
        void black_deal(int mid, int[,] imgb)
        {
            int i = 0, j = 0, num_F = 0;


            for (i = 57 - mid; i > 0; i--)
            {
                if (BlackLineData[i + 2] - 5 > 0)
                    for (j = BlackLineData[i + 2] - 5; j <= BlackLineData[i + 2] + 5; j++)
                    {
                        if (j < 80 && j >= 0)
                            if (imgb[i, j] == 0)
                                break;
                    }
                if (j > BlackLineData[i + 2] + 5)
                {
                    break;
                }
                else
                {
                    BlackLineData[i + 1] = j;
                    num_F++;
                    LeftBlack[i + 1] = j - 25;
                    RightBlack[i + 1] = j + 25;
                }
            }
            if (num_F >= 3)
            {
                StableNumbers = num_F;
            }


        }






        //2017年3月27日19:31:23理解：边缘检测（中线用）、中线获取
        void GetLMR()
        {
            int i = 0, j = 0;
            int temLeft = 0;
            int temRight = 0;
            int pLeft = OV7725_EAGLE_W / 2, pRight = OV7725_EAGLE_W / 2;
            bool bFoundLeft = false;
            bool bFoundRight = false;
            int TripLen = 4;
            bool MidEnd = false;
            int MidToBlackCount = 0;
            int line1 = 0;
            int[] jump = new int[30], jump_mid = new int[29], jump_B = new int[10]; int j_save = 0;

            int flag_wob = 0;

            NoValidCount = 0;
            NoValidMax = 0;
            StableNumbers = 0;
            LeftStableNumbers = 0;
            RightStableNumbers = 0;
            ValidLineCount1 = 0;
            ValidLineCount2 = 0;


            
            for (i = OV7725_EAGLE_H - 1; i >= 0 && !MidEnd; i--)//最近一行开始
            {
                // 初始化指针

                if (i < OV7725_EAGLE_H - 5)//5行以上
                {
                    pLeft = BlackLineData[i + 1];//利用下一行边缘继续寻线
                    pRight = BlackLineData[i + 1];//利用下一行边缘继续寻线
                }
                else
                {
                    if (BlackEndL < 5 && BlackEndR > BlackEndM)//左侧有效点数小于5，图像右偏
                    {
                        pLeft = OV7725_EAGLE_W * 3 / 4 - 1;//图像遍历指针给大
                        pRight = OV7725_EAGLE_W * 3 / 4 - 1;
                    }
                    else if (BlackEndR < 5 && BlackEndL > BlackEndM)//右侧有效点数小于5，图像左偏
                    {
                        pLeft = OV7725_EAGLE_W / 4 - 1;
                        pRight = OV7725_EAGLE_W / 4 - 1;
                    }
                    else
                    {
                        pLeft = OV7725_EAGLE_W / 2 - 1;
                        pRight = OV7725_EAGLE_W / 2 - 1;
                    }
                }


                // 初始化标记
                bFoundLeft = bFoundRight = false;

                //两边黑线提取
                for (j = 0; j < OV7725_EAGLE_W; j++)
                {
                    // 往左搜索
                    if (bFoundLeft == false && pLeft > 0)
                    {
                        //未找到左边缘则寻找
                        if (IMG_BUFF[i, pLeft] == White && IMG_BUFF[i, pLeft - 1] == Black)
                        {
                            LeftBlack[i] = pLeft - 1;
                            if (LeftBlack[i] > 0)
                            {
                                bFoundLeft = true;//找到左边缘
                            }
                            if (bFoundRight)
                            {
                                break;//右边缘也找到
                            }
                        }
                        else
                        {
                            pLeft--;
                        }
                    }// if结束--从左到右搜索边缘

                    //往右搜索
                    if (bFoundRight == false && pRight < OV7725_EAGLE_W - 1)
                    {
                        //未找到右边缘
                        if (IMG_BUFF[i, pRight] == White && IMG_BUFF[i, pRight + 1] == Black)
                        {
                            RightBlack[i] = pRight + 1;
                            if (RightBlack[i] < OV7725_EAGLE_W - 1)
                            {
                                bFoundRight = true;//找到右边缘
                            }
                            if (bFoundLeft)
                            {
                                break;// 找到左边缘
                            }
                        }
                        else
                        {
                            pRight++;
                        }
                    }// if结束
                }//for结束


                if (i < OV7725_EAGLE_H - 6 && !bFoundLeft && !bFoundRight)//未找到边缘
                {
                    ValidLine[i] = 0;
                    NoValidCount++;//无效行计数
                    if (NoValidCount > NoValidMax)
                    {
                        NoValidMax = NoValidCount;//无效行计数最大值更新
                    }
                }
                else
                {
                    NoValidCount = 0;
                }

                if (bFoundLeft && bFoundRight)
                {
                    ValidLineCount1++;
                    ValidLine[i] = 3;//该行找到双边缘
                }
                else
                {
                    if (bFoundLeft)
                    {
                        ValidLineCount2++;
                        if (ValidLineCount2 == 8) ;
                        ValidLine[i] = 1;
                        if (g_Derict == Angle)
                        {
                            ValidLine[i] = 4;//右直角

                            //ke=35;
                        }
                    }
                    if (bFoundRight)
                    {
                        ValidLineCount2++;
                        if (ValidLineCount2 == 8) ;
                        ValidLine[i] = 1;
                        if (g_Derict == Angle)
                        {
                            ValidLine[i] = 5;//左直角

                            //ke=-35;
                        }
                    }
                }

                if (!bFoundLeft) //未找到左边缘
                {

                    if (i < 35)
                    {
                        LeftBlack[i] = LeftBlack[i + 1] + LeftBlack[i + 1] - LeftBlack[i + 2]; //进行补线
                    }
                    else
                    {
                        LeftBlack[i] = 0;
                    }
                }
                else if (i < 35 && ABS(LeftBlack[i] - LeftBlack[i + 1]) > TripLen)
                {
                    LeftBlack[i] = LeftBlack[i + 1] + LeftBlack[i + 1] - LeftBlack[i + 2];//跳变补线

                }

                if (!bFoundRight)//未找到右边缘
                {

                    if (i < 35)
                    {
                        RightBlack[i] = RightBlack[i + 1] + RightBlack[i + 1] - RightBlack[i + 2];//进行补线
                    }
                    else
                    {
                        RightBlack[i] = OV7725_EAGLE_W - 1;
                    }
                }
                else if (i < 35 && ABS(RightBlack[i] - RightBlack[i + 1]) > TripLen)
                {
                    RightBlack[i] = RightBlack[i + 1] + RightBlack[i + 1] - RightBlack[i + 2];//跳变补线

                }

                if (LeftBlack[i] > RightBlack[i])
                {
                    temRight = temLeft = (LeftBlack[i] + RightBlack[i]) / 2;
                    LeftBlack[i] = temLeft;
                    RightBlack[i] = temRight;
                }

                LeftStableNumbers++;
                RightStableNumbers++;
                BlackLineData[i] = LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2;
                //中心搜索结束

                //中心线数组数据处理
                if (BlackLineData[i] < 4 || BlackLineData[i] > OV7725_EAGLE_W - 4)
                {
                    MidEnd = true;
                    break;
                }
                if (i < OV7725_EAGLE_H - 20 && ABS(BlackLineData[i] - BlackLineData[i + 1]) > TripLen)//有跳变，中部图像
                {
                    BlackLineData[i] = BlackLineData[i + 1] + BlackLineData[i + 1] - BlackLineData[i + 2];//进行中心线平均
                }
                if (BlackLineData[i] > 79) BlackLineData[i] = 79;
                if (BlackLineData[i] < 0) BlackLineData[i] = 0;
                if (IMG_BUFF[i, BlackLineData[i]] == Black)//中间截至
                {
                    if (ABS(BlackLineData[i] - OV7725_EAGLE_M) <= 3)
                    {
                        ;//什么也不做
                    }
                    else
                    {
                        MidToBlackCount++;
                        if (MidToBlackCount >= 2)
                        {
                            MidEnd = true;
                        }
                    }
                }
                else
                {
                    MidToBlackCount = 0;
                }
                if (!MidEnd)
                {
                    StableNumbers++;
                }
                if (ValidLineCount1 < 4 && i < 20)
                {
                    MidEnd = true;
                }
                if (ValidLineCount2 > 15)
                {
                    ;
                }
            }//for结束-- 行扫描完

            /******************************************************************
            **********************************************************************
            *********************************************************************/
            flag_wob = IMG_BUFF[57, 0];
            j = 1;
            jump[0] = 0;
            num = 1;
            for (i = 0; i < 80; i++)
            {
                if (IMG_BUFF[57, i] != flag_wob)
                {
                    jump[j++] = i;
                    flag_wob = IMG_BUFF[57, i];
                }
            }
            jump[j] = 79;
            i = j - 1;
            if (i >= 2)
            {
                j_save = j;
                for (; j_save > 0; j_save--)
                {
                    jump_mid[j_save - 1] = (jump[j_save] - jump[j_save - 1]) / 2 + jump[j_save - 1];//将所有的中线都保存在jump_mid数组中
                }
                j_save = 0;
                line1 = i;
                //暂时保存一下
                for (line1--; line1 > 0; line1--)
                {
                    if (IMG_BUFF[57, jump_mid[line1]] == 0 && jump[line1 + 1] - jump[line1] < 9)
                    {
                        j_save++;
                        jump_B[j_save] = jump_mid[line1];
                    }

                }
                i = 0;
                while (j_save > 0)
                {
                    if (ABS(i - 39) > ABS(jump_B[j_save] - 39))
                        i = jump_B[j_save];
                    j_save--;
                }

            }
            else
            {
                i = 0;                // 标志位
            }
            if (i != 0)
            {
                BlackLineData[59] = i;
                LeftBlack[59] = i - 25;
                RightBlack[59] = i + 25;
                black_deal(0, IMG_BUFF);
            }



        }

        /******************************************************************************
        //滤波函数
        ******************************************************************************/
        int P0_X = 0;
        int P0_Y = 0;
        int P1_X = 0;
        int P1_Y = 0;
        int P2_X = 0;
        int P2_Y = 0;
        float Mid_K1 = 0.0f;
        float Mid_K2 = 0.0f;

        //左边缘滤波
        void LAverageFilter()
        {
            int i = 0;
            int j = 0;
            int sum = 0;
            for (i = OV7725_EAGLE_H - 1; i > OV7725_EAGLE_H - (LeftStableNumbers - 5); i--)//有效行后5行去掉进行滤波
            {
                sum = 0;
                for (j = 0; j < 5; j++)
                {
                    sum += LeftBlack[i - j];
                }
                LeftBlack[i] = sum / 5;//进行平均
            }
            if (OV7725_EAGLE_H - (LeftStableNumbers - 6) < 60)
                P1_X = LeftBlack[OV7725_EAGLE_H - (LeftStableNumbers - 6)];//最后一个有效点的列数
            P1_Y = OV7725_EAGLE_H - (LeftStableNumbers - 6);//最后一个有效点的行数
        }

        //右边缘滤波
        void RAverageFilter()
        {
            uint i = 0;
            uint j = 0;
            int sum = 0;
            for (i = OV7725_EAGLE_H - 1; i > OV7725_EAGLE_H - (RightStableNumbers - 5); i--)
            {
                sum = 0;
                for (j = 0; j < 5; j++)
                {
                    sum += RightBlack[i - j];
                }
                RightBlack[i] = sum / 5;
            }
            if (OV7725_EAGLE_H - (RightStableNumbers - 6) < 60 && OV7725_EAGLE_H - (RightStableNumbers - 6) > 0)
                P2_X = RightBlack[OV7725_EAGLE_H - (RightStableNumbers - 6)];
            P2_Y = OV7725_EAGLE_H - (RightStableNumbers - 6);

        }

        //中心滤波
        void AverageFilter()
        {
            uint i = 0;
            uint j = 0;
            int sum = 0;
            for (i = OV7725_EAGLE_H - 1; i > OV7725_EAGLE_H - (StableNumbers - 5 - 5); i--)
            {
                sum = 0;
                for (j = 0; j < 5; j++)
                {
                    sum += BlackLineData[i - j];
                }
                BlackLineData[i] = sum / 5;
            }
            P0_X = BlackLineData[OV7725_EAGLE_H - 1];
            P0_Y = OV7725_EAGLE_H - 1;

            Mid_K1 = (float)(ABS(P0_X - P1_X) * 1.0 / ABS(P0_Y - P1_Y));//斜率1
            Mid_K2 = (float)(ABS(P0_X - P2_X) * 1.0 / ABS(P0_Y - P2_Y));//斜率2

        }
        /******************************************************************************
        //通过左右边缘来获得舵机控制中心线
        ******************************************************************************/
        void GetFinalMidLine()
        {
            int i = 0, MinStable = 0;
            MinStable = MIN(StableNumbers, LeftStableNumbers);
            MinStable = MIN(MinStable, RightStableNumbers);//最小有效行数

            for (i = OV7725_EAGLE_H - 1; i > OV7725_EAGLE_H - (MinStable - 5); i--)//有效行后5行去掉进行滤波
            {
                BlackLineData[i] = LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2;//中线数据

                if (BlackLineData[i] > OV7725_EAGLE_W - 1)
                {
                    BlackLineData[i] = OV7725_EAGLE_W - 1;//限幅
                }
                else if (BlackLineData[i] < 0)
                {
                    BlackLineData[i] = 0;//限幅
                }
            }

            if (StableNumbers > MinStable)//中心线数据有效
            {
                for (i = OV7725_EAGLE_H - (MinStable - 5); i > OV7725_EAGLE_H - (StableNumbers - 5); i--)//滤掉的5行数据在处理
                {
                    if (i < 58)
                    {
                        BlackLineData[i] = BlackLineData[i + 1] + BlackLineData[i + 1] - BlackLineData[i + 2];
                        if (BlackLineData[i] > OV7725_EAGLE_W - 1)
                        {
                            BlackLineData[i] = OV7725_EAGLE_W - 1;
                        }
                        else if (BlackLineData[i] < 0)
                        {
                            BlackLineData[i] = 0;
                        }
                    }
                }
            }
        }

        /******************************************************************************
        //中心线补偿
        ******************************************************************************/

        int CompensateCount = 0;
     /************************************2017年4月2日11:40:31******************************************/
        //发现会造成左右偏差过大
        void MidLineCompensate()
        {
            int i = 0, icount = 0, j = 0;
            int CompensateData = 0;

            int sum = 0;
            float avg = 0.0f;
            int tem = 1;//正负补偿标志

            CompensateCount = 0;

            for (i = OV7725_EAGLE_H - 2, icount = 0; i > OV7725_EAGLE_H - (StableNumbers - 10); i--, icount++)
            {
                sum += (BlackLineData[i] - BlackLineData[i + 1]);//进行所有中线数据平均偏离数据类和
            }
            avg = sum * 1.0f / icount;//平均

            if (avg < 0)
            {
                tem = -1;
                avg = (-1) * avg;
            }
            if (avg > 1.0)
            {
                CompensateData = 3;//偏离程度大，补偿量大
            }
            else if (avg > 0.35)
            {
                CompensateData = 2;//偏离程度中，补偿量中
            }
            else if (avg > 0.25)
            {
                CompensateData = 1;//偏离程度小，补偿量小
            }
            else
            {
                CompensateData = 0;
            }

            CompensateData = CompensateData * tem;

            for (i = OV7725_EAGLE_H - (StableNumbers - 10); i > 0; i--)
            {

                BlackLineData[i] = BlackLineData[i + 1] + CompensateData;

                CompensateCount++;
                if (BlackLineData[i] > 79) BlackLineData[i] = 79;
                if (BlackLineData[i] < 0) BlackLineData[i] = 0;
                if (IMG_BUFF[i, BlackLineData[i]] == Black || BlackLineData[i] < 2 || BlackLineData[i] > OV7725_EAGLE_W - 2)
                {
                    break;
                }
                sum = 0;
                for (j = OV7725_EAGLE_H - 2, icount = 0; j > i; j--, icount++)
                {
                    sum += (BlackLineData[j] - BlackLineData[j + 1]);
                }
                avg = sum * 1.0f / icount;
                if (avg < 0)
                {
                    tem = -1;
                    avg = (-1) * avg;
                }
                if (avg > 1.0)
                {
                    CompensateData = 3;
                }
                else if (avg > 0.55)
                {
                    CompensateData = 2;
                }
                else if (avg > 0.25)
                {
                    CompensateData = 1;
                }
                else
                {
                    CompensateData = 0;
                }
                CompensateData = CompensateData * tem;
            }
        }

        int[] TemMidLineData = new int[OV7725_EAGLE_H];//提取黑线值数据

        void StoreMidLine()
        {
            int i = 0;
            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                TemMidLineData[i] = BlackLineData[i];//中心线转存	
            }
        }

        int TopE1 = 0;//中心线上部分偏差
        int TopE2 = 0;//中心线下部分偏差
        int TopLen1 = 0;//中心线上部分偏差
        int TopLen2 = 0;//中心线上部分偏差
        int SubBasePoint = 0;//下部分基准点最大偏差

        void GetSectionParam()
        {
            int TotalPoint = StableNumbers - 10;//可以修改，这个是分段算偏差的。
            int icount = 0;
            int i = 0;
            int BasePoint = BlackLineData[OV7725_EAGLE_H - 1];
            SubBasePoint = 0;
            TopE1 = 0;
            TopE2 = 0;
            TopLen1 = 0;
            TopLen2 = 0;

            for (i = OV7725_EAGLE_H - (StableNumbers - 10), icount = 1; i < OV7725_EAGLE_H - 2; i++, icount++)
            {
                if (icount < TotalPoint / 2)
                {
                    TopE1 += (BlackLineData[i] - BlackLineData[i + 1]);//上部分图像偏差类和
                    TopLen1++;//标志位累加
                }
                else
                {
                    TopE2 += (BlackLineData[i] - BlackLineData[i + 1]);
                    TopLen2++;
                    if (ABS(BlackLineData[i] - BasePoint) > ABS(SubBasePoint))
                    {
                        SubBasePoint = BlackLineData[i] - BasePoint;//相邻中心点误差跟新
                    }
                }
            }
        }

        /******************************************************************************
        //获取中心线偏差
        ******************************************************************************/


        double MidLineVariance = 0.0;
        int MidLineExcursion = 0;

        void GetMidLineVariance()
        {
            int i = 0;
            int iCount = 0;
            int Black_Sum = 0;
            float aver = 0.0f;
            int end = OV7725_EAGLE_H - (StableNumbers - 5);

            MidLineExcursion = 0;//偏移量置零

            for (i = OV7725_EAGLE_H - 2, iCount = 0; i > end; i--, iCount++)
            {
                Black_Sum += BlackLineData[i];//累和
                MidLineExcursion = MidLineExcursion + BlackLineData[i] - BlackLineData[i + 1];//中线偏移量类和
            }
            aver = Black_Sum * 1.0f / iCount;//中线偏移量类和平均

            MidLineVariance = 0.0;//方差给0

            for (i = OV7725_EAGLE_H - 2; i > end; i--)
            {
                MidLineVariance += (aver - BlackLineData[i]) * (aver - BlackLineData[i]);//方差类和
            }
            MidLineVariance = MidLineVariance * 1.0 / iCount;//平均方差
        }

        /******************************************************************************
        //获取特殊中心线偏差
        ******************************************************************************/
        void GetSpecialError()
        {
            int i = 0;
            int end = OV7725_EAGLE_H - StableNumbers;

            MidLineExcursion = 0;
            for (i = OV7725_EAGLE_H - 1; i > end; i--)
            {
                BlackLineData[i] = LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2;
            }
            for (i = OV7725_EAGLE_H - 2; i > end; i--)
            {
                MidLineExcursion = MidLineExcursion + BlackLineData[i] - BlackLineData[i + 1];
            }

            MidLineVariance = 300;

            if (MidLineExcursion > 0)
            {
                MidLineExcursion = 40;
            }
            else if (MidLineExcursion < 0)
            {
                MidLineExcursion = -40;
            }
        }

        /******************************************************************************
        //获取十字中心线
        ******************************************************************************/
        int g_BasePos = OV7725_EAGLE_W / 2 - 1;
        int CrossingStable = 0;
        int[] ValidLineR = new int[OV7725_EAGLE_H]; //十字右边行有效标志数组
        int[] ValidLineL = new int[OV7725_EAGLE_H];//十字左边行有效标志数组
        int NoValidLMax = 0;//十字交叉左边连续丢线计数
        int NoValidRMax = 0;//十字交叉右边连续丢线计数

        void GetCrossingMidLine()
        {
            int i = 0, j = 0;
            //指针
            int pLeft = OV7725_EAGLE_W / 2, pRight = OV7725_EAGLE_W / 2;
            bool bFoundLeft = false;
            bool bFoundRight = false;
            int temLeft = 0, temRight = 0, temi = 0;
            int temBasePos = 0;

            bool EndFlag = false;
            int LCount = 0;
            int RCount = 0;
            bool bFoundFlag = false;

            CrossingStable = 0;
            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                ValidLineR[i] = 0;
                ValidLineL[i] = 0;
            }

            //十字左右倾
            if (BlackEndMax == BlackEndL)
            {
                g_Derict = L_BlackEnd;//左倾
            }
            else if (BlackEndMax == BlackEndR)
            {
                g_Derict = R_BlackEnd;//右倾
            }
            else if (BlackEndMax == BlackEndM)
            {
                if (ABS(BlackEndM - BlackEndR) <= 2 && ABS(BlackEndM - BlackEndL) <= 2)
                {
                    g_Derict = Angle;//直角
                }
                else if (ABS(BlackEndL - BlackEndR) < 5)     ////////////
                {
                    g_Derict = M_BlackEnd;//正十字
                }
                else if (BlackEndL > BlackEndR)
                {
                    g_Derict = L_BlackEnd;//左
                }
                else
                {
                    g_Derict = R_BlackEnd;//右
                }
            }

            //搜索十字边线
            for (i = OV7725_EAGLE_H - 1; i > 0; i--)
            {
                if (!EndFlag)
                {
                    CrossingStable++;
                }
                //除去15行从中间往两边搜
                if (i > OV7725_EAGLE_H - 15)
                {
                    //初始化指针
                    pLeft = OV7725_EAGLE_W / 2 - 1;
                    pRight = OV7725_EAGLE_W / 2 - 1;
                }
                else
                {
                    //初始化指针
                    pLeft = g_BasePos;
                    pRight = g_BasePos;
                }
                //初始化搜索到标记
                bFoundLeft = bFoundRight = false;

                for (j = 0; j < OV7725_EAGLE_W; j++)
                {
                    //往左搜索
                    if (bFoundLeft == false && pLeft > 0)
                    {
                        if ((IMG_BUFF[i, pLeft] == White && IMG_BUFF[i, pLeft - 1] == Black) || pLeft == 1)
                        {
                            //找到左边缘
                            LeftBlack[i] = pLeft - 1;
                            bFoundLeft = true;

                            if (LeftBlack[i] > 0)
                            {
                                ValidLineL[i] = 1;
                                LCount = 0;
                            }
                            else
                            {
                                LCount++;
                                if (LCount > NoValidLMax)
                                {
                                    //左边连续丢线总数
                                    NoValidLMax = LCount;
                                }
                            }
                            //右边缘找到
                            if (bFoundRight)
                            {
                                break;
                            }
                        }
                        else
                        {
                            pLeft--;
                        }
                    }// if结束--从左到右搜索结束

                    //往右搜索
                    if (bFoundRight == false && pRight < OV7725_EAGLE_W - 1)
                    {
                        //未找到右边缘则寻找
                        if ((IMG_BUFF[i, pRight] == White && IMG_BUFF[i, pRight + 1] == Black) || pRight == OV7725_EAGLE_W - 2)
                        {
                            // 找到右边缘
                            RightBlack[i] = pRight + 1;
                            bFoundRight = true;
                            if (RightBlack[i] < OV7725_EAGLE_W - 1)
                            {
                                ValidLineR[i] = 1;
                                RCount = 0;
                            }
                            else
                            {
                                RCount++;
                                if (RCount > NoValidRMax)
                                {
                                    NoValidRMax = RCount;
                                }
                            }

                            // 左边缘也找到
                            if (bFoundLeft)
                            {
                                break;
                            }
                        }
                        else
                        {
                            pRight++;
                        }
                    }// if结束
                }//for结束

                //左找不到边缘
                if (!bFoundLeft)
                {
                    LeftBlack[i] = 0;
                }
                //右找不到边缘
                if (!bFoundRight)
                {
                    RightBlack[i] = OV7725_EAGLE_W - 1;
                }

                if (i < OV7725_EAGLE_H - 15)
                {
                    //左倾情况搜索，中心线往左
                    if (g_Derict == L_BlackEnd)
                    {
                        //找到的中心线位置偏左，作为新的搜索基点
                        if (LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2 < g_BasePos && ABS((LeftBlack[i] - LeftBlack[i + 1])) < 3 && ABS((RightBlack[i] - RightBlack[i + 1])) < 3)
                        {
                            temBasePos = LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2;
                            if (ABS(temBasePos - g_BasePos) < 20)
                            {
                                g_BasePos = temBasePos;
                                if (g_BasePos < 2)
                                {
                                    EndFlag = true;
                                }
                            }
                        }
                        //找到的中心线位置偏右，作为新的搜索基点
                        else if (LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2 > g_BasePos || RightBlack[i] > RightBlack[i + 1] + 2)
                        {
                            temLeft = 0;
                            temRight = 0;
                            bFoundFlag = false;
                            //确定新的搜索基点
                            for (temi = 1; temi < OV7725_EAGLE_W - 1; temi++)
                            {
                                if (IMG_BUFF[i, temi] == White && IMG_BUFF[i, temi + 1] == White && temLeft == 0)
                                {
                                    temLeft = temi;
                                }
                                if (temLeft != 0)
                                {
                                    if(temi+5<80)//2017年4月9日18:44:20修改
                                    if (IMG_BUFF[i, temi] == Black && IMG_BUFF[i, temi + 1] == Black && IMG_BUFF[i, temi + 5] == Black)
                                    {
                                        temRight = temi;
                                        bFoundFlag = true;
                                        break;
                                    }
                                }
                            }
                            if (bFoundFlag && temLeft + (temRight - temLeft) / 2 < g_BasePos)
                            {
                                temBasePos = temLeft + (temRight - temLeft) / 2;
                                g_BasePos = temBasePos;
                                if (g_BasePos < 3)
                                {
                                    //已到最左边无需搜素
                                    EndFlag = true;
                                }
                            }
                        }
                    }

                    //十字右倾
                    if (g_Derict == R_BlackEnd)
                    {
                        //找到的中心线位置偏右，作为新的搜索基点
                        if (LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2 > g_BasePos && ABS((LeftBlack[i] - LeftBlack[i + 1])) < 3 && ABS((RightBlack[i] - RightBlack[i + 1])) < 3)
                        {
                            temBasePos = LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2;
                            if (ABS(temBasePos - g_BasePos) < 20)
                            {
                                g_BasePos = temBasePos;
                                if (g_BasePos > OV7725_EAGLE_W - 4)
                                {
                                    EndFlag = true;
                                }
                            }
                        }
                        //找到的中心线位置偏左，作为新的搜索基点 
                        else if (LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2 < g_BasePos || LeftBlack[i] < LeftBlack[i + 1] - 2)
                        {
                            temLeft = 0;
                            temRight = 0;
                            bFoundFlag = false;
                            //确定新的搜索基点
                            for (temi = OV7725_EAGLE_W - 1; temi > 4; temi--)
                            {
                                if (IMG_BUFF[i, temi] == White && IMG_BUFF[i, temi - 1] == White && temRight == 0)
                                {
                                    temRight = temi;
                                }
                                if (temRight != 0)
                                {
                                    if (IMG_BUFF[i, temi] == Black && IMG_BUFF[i, temi - 1] == Black && IMG_BUFF[i, temi - 5] == Black)
                                    {
                                        temLeft = temi;
                                        bFoundFlag = true;
                                        break;
                                    }
                                }
                            }
                            if (bFoundFlag && temLeft + (temRight - temLeft) / 2 > g_BasePos)
                            {
                                temBasePos = temLeft + (temRight - temLeft) / 2;
                                g_BasePos = temBasePos;
                                if (g_BasePos > OV7725_EAGLE_W - 3)
                                {
                                    EndFlag = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        /******************************************************************************
        //曲线拟合
        ******************************************************************************/
        void CommonRectificate(int[] data, int begin, int end)
        {
            int MidPos = 0;
            if (end > OV7725_EAGLE_H - 1)
            {
                end = OV7725_EAGLE_H - 1;
            }
            if (begin == end)
            {
                data[begin] = (data[begin - 1] + data[begin + 1]) / 2;
                BlackLineData[begin] = LeftBlack[begin] + (RightBlack[begin] - LeftBlack[begin]) / 2;
            }
            else if (begin < end)
            {
                MidPos = (begin + end) / 2;
                data[MidPos] = (data[begin] + data[end]) / 2;
                BlackLineData[MidPos] = LeftBlack[MidPos] + (RightBlack[MidPos] - LeftBlack[MidPos]) / 2;
                if (begin + 1 < MidPos)
                {
                    CommonRectificate(data, begin, MidPos);
                }
                if (MidPos + 1 < end)
                {
                    CommonRectificate(data, MidPos, end);
                }
            }
        }


        /******************************************************************************
        //正十字处理
        ******************************************************************************/
        bool IsCrossing = false;

        void SCProcessing()
        {
            int i = 0;
            int startPos = 0, endPos = 0, temCount = 0, countMax = 0, temPos = 0;
            int ProcessFlag = 0;

            //ì跳变计数
            int TripPointCount = 0;
            int TripLen = 2;
            //获取十字边缘线
            GetCrossingMidLine();

            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                TripPointPos[i] = 0;
            }
            TripPointCount = 0;
            //根据跳变分段
            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                // 中心做差，找出跳变出，分段
                if (i > 0)
                {
                    SubValue[i] = LeftBlack[i] - LeftBlack[i - 1];
                    //跳变判断
                    if (ABS(SubValue[i]) > TripLen || LeftBlack[i] < 2)
                    {
                        //记录跳变位置
                        TripPointPos[TripPointCount] = i;
                        TripPointCount++;
                    }
                }
            }//for结束
            TripPointPos[TripPointCount] = OV7725_EAGLE_H;
            temCount = 0;
            countMax = 0;
            temPos = TripPointPos[0] - 1;
            startPos = temPos;
            endPos = 0;
            for (i = 1; i < TripPointCount; i++)
            {
                if (TripPointPos[i] - TripPointPos[i - 1] < 3)//两次跳变行之间的行号之差
                {
                    temCount++;
                    if (temCount > countMax)
                    {
                        countMax = temCount;
                        startPos = temPos;
                        endPos = TripPointPos[i] + 1;
                    }
                }
                else
                {
                    temPos = TripPointPos[i] - 1;
                    temCount = 0;
                }
            }//End for

            if (startPos > 10)
            {
                CommonRectificate(LeftBlack, startPos, endPos);
                ProcessFlag = 1;
            }

            //右边缘补线
            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                TripPointPos[i] = 0;
            }
            TripPointCount = 0;
            //根据跳变分段
            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                // 中心做差，找出跳变出，分段
                if (i > 0)
                {
                    SubValue[i] = RightBlack[i] - RightBlack[i - 1];
                    //跳变判断
                    if (ABS(SubValue[i]) > TripLen || RightBlack[i] > OV7725_EAGLE_W - 3)
                    {
                        //记录跳变位置
                        TripPointPos[TripPointCount] = i;
                        TripPointCount++;
                    }
                }
            }//for结束

            TripPointPos[TripPointCount] = OV7725_EAGLE_H;
            temCount = 0;
            countMax = 0;

            endPos = 0;
            temPos = TripPointPos[0] - 1;
            startPos = temPos;
            for (i = 1; i < TripPointCount; i++)
            {
                if (TripPointPos[i] - TripPointPos[i - 1] < 3)
                {
                    temCount++;
                    if (temCount > countMax)
                    {
                        countMax = temCount;
                        startPos = temPos;
                        endPos = TripPointPos[i] + 1;
                    }
                }
                else
                {
                    temPos = TripPointPos[i] - 1;
                    temCount = 0;
                }
            }//End for

            if (startPos > 10)
            {
                //拟合
                CommonRectificate(RightBlack, startPos, endPos);
                ProcessFlag = 1;

            }
            if (0 == ProcessFlag)
            {
                IsCrossing = false;
            }
        }


        /******************************************************************************
        //十字处理
        ******************************************************************************/

        int LCrossingTripPos = 0;
        int RCrossingTripPos = 0;
        int bFoundTripPoint = 0;//找到十字跳变点


        void ProcessCrossing()
        {
            int i = 0, iStart = OV7725_EAGLE_H - StableNumbers + 6, iEnd = OV7725_EAGLE_H - 1;
            int iCount = 0;
            int TripPos = 0, pos = 0;
            int Count1 = 0, Count2 = 0;
            int tem0 = 0, tem1 = 0;
            int startPos = 0, endPos = 0;
            int ProcessFlag = 0;

            LCrossingTripPos = 0;
            RCrossingTripPos = 0;

            bFoundTripPoint = 0;
            if (iStart < 5)
            {
                iStart = 5;
            }
            for (i = iStart; i < iEnd; i++)
            {
                tem0 = RightBlack[i] > OV7725_EAGLE_W - 1 ? OV7725_EAGLE_W - 1 : RightBlack[i];
                tem1 = LeftBlack[i] < 0 ? 0 : LeftBlack[i];
                if (tem0 - tem1 > 70)//判定十字空白行阈值
                {
                    iCount++;
                }
                else
                {
                    break;
                }
            }

            if (iCount > 30)//空白行计数达到7
            {
                IsCrossing = true;//标志位置位
            }
            else
            {

                if (NoValidMax > 30)//无效行计数达到8
                {
                    IsCrossing = true;//标志位置位

                    if (ValidLine[OV7725_EAGLE_H - 7] == 0)//判断57行线型，是否是没有找到双边
                    {
                        SCProcessing();//正十字处理
                        return;
                    }
                    else
                    {
                        SCProcessing();//正十字处理
                        return;
                    }
                }
                else
                {
                    IsCrossing = false;//标志位清0
                    return;
                }
            }
            //正十字
            if (iCount > 30 && g_Derict == M_BlackEnd)
            {
                SCProcessing();
                return;
            }

            //右倾找左边缘跳变
            if (iCount > 30 && g_Derict == R_BlackEnd)
            {
                i = iStart;
                while (i < iEnd && LeftBlack[i] - LeftBlack[i + 1] == 0)
                {
                    i++;
                }
                Count1 = 0;
                Count2 = 0;
                if (i > 0 && i < 59)
                    if (LeftBlack[i] - LeftBlack[i + 1] > 0)
                    {
                        Count1++;
                        i++;
                        for (; i < iEnd; i++)
                        {
                            if (LeftBlack[i] - LeftBlack[i + 1] >= 0)
                            {
                                Count1++;
                                if (Count2 != 0)
                                {
                                    Count1 = 1;
                                    Count2 = 0;
                                }
                            }
                            else if (LeftBlack[i] - LeftBlack[i + 1] < 0)
                            {
                                if (Count1 > 2 && TripPos == 0)
                                {
                                    TripPos = i;
                                }
                                Count2++;
                            }
                        }
                        if (Count1 > 2 && Count2 > 2)
                        {
                            bFoundTripPoint = 1;
                            LCrossingTripPos = TripPos;//左边缘跳变找到
                        }
                        else
                        {
                            Count1++;
                            i++;
                            for (; i < iEnd; i++)
                            {
                                if (LeftBlack[i] - LeftBlack[i + 1] < 0)
                                {
                                    Count1++;
                                    if (Count2 != 0)
                                    {
                                        Count1 = 1;
                                        Count2 = 0;
                                    }
                                }
                                else if (LeftBlack[i] - LeftBlack[i + 1] > 0)
                                {
                                    if (Count1 > 2 && TripPos == 0)
                                    {
                                        TripPos = i;
                                    }
                                    Count2++;
                                }
                            }
                            if (Count1 > 2 && Count2 > 2)
                            {
                                bFoundTripPoint = 1;
                                LCrossingTripPos = TripPos;//左边缘跳变找到
                            }
                        }

                        //左倾找右边缘跳变
                        if (iCount > 30 && g_Derict == L_BlackEnd)
                        {
                            i = iStart;
                            Count1 = 0;
                            Count2 = 0;
                            while (i < iEnd && RightBlack[i] - RightBlack[i + 1] == 0)
                            {
                                i++;
                            }
                            if (RightBlack[i] - RightBlack[i + 1] > 0)
                            {
                                Count1++;
                                i++;
                                for (; i < iEnd; i++)
                                {

                                    if (RightBlack[i] - RightBlack[i + 1] > 0)
                                    {
                                        Count1++;
                                        if (Count2 != 0)
                                        {
                                            Count1 = 1;
                                            Count2 = 0;
                                        }
                                    }
                                    else if (RightBlack[i] - RightBlack[i + 1] < 0)
                                    {
                                        if (Count1 > 2 && TripPos == 0)
                                        {
                                            TripPos = i;
                                        }
                                        Count2++;
                                    }
                                }
                                if (Count1 > 2 && Count2 > 2)
                                {
                                    bFoundTripPoint = 1;
                                    RCrossingTripPos = TripPos;//右边缘跳变找到
                                }
                            }
                            else
                            {
                                Count1++;
                                i++;
                                for (; i < iEnd; i++)
                                {
                                    if (RightBlack[i] - RightBlack[i + 1] < 0)
                                    {
                                        Count1++;
                                        if (Count2 != 0)
                                        {
                                            Count1 = 1;
                                            Count2 = 0;
                                        }
                                    }
                                    else if (RightBlack[i] - RightBlack[i + 1] > 0)
                                    {
                                        if (Count1 > 2 && TripPos == 0)
                                        {

                                        }
                                        Count2++;
                                    }
                                }
                                if (Count1 > 2 && Count2 > 2)
                                {
                                    bFoundTripPoint = 1;
                                    RCrossingTripPos = TripPos;//右边缘跳变找到
                                }
                            }
                        }
                    }
                if (bFoundTripPoint != 0)
                {
                    GetCrossingMidLine();//取十字边缘线
                    if (g_Derict == L_BlackEnd)//左倾
                    {
                        pos = TripPos - 8;
                        while (pos > 0 && (ValidLineR[pos] == 0 || RightBlack[pos] > RightBlack[TripPos]))
                        {
                            pos--;
                        }
                        if (RightBlack[pos - 2] < RightBlack[TripPos])//拟合
                        {
                            //右边缘拟合
                            CommonRectificate(RightBlack, pos - 2, TripPos);
                            ProcessFlag = 1;
                        }
                        else
                        {
                            //另一种拟合
                            for (i = 0; i < OV7725_EAGLE_W; i++)
                            {
                                if (IMG_BUFF[pos - 2, i] == White && IMG_BUFF[pos - 2, i + 1] == Black)
                                {
                                    RightBlack[pos - 2] = i;
                                    break;
                                }
                            }
                            if (RightBlack[pos - 2] < RightBlack[TripPos])//拟合
                            {
                                //右边缘拟合
                                CommonRectificate(RightBlack, pos - 2, TripPos);
                                ProcessFlag = 1;
                            }
                            else if (NoValidLMax > 25)
                            {
                                RightBlack[pos - 2] = 2;
                                CommonRectificate(RightBlack, pos - 2, TripPos);
                                ProcessFlag = 1;
                            }
                        }
                    }
                    else if (g_Derict == R_BlackEnd)//右倾
                    {
                        pos = TripPos - 8;
                        while (pos > 0 && ValidLineL[pos] == 0 || LeftBlack[pos] < LeftBlack[TripPos])
                        {
                            pos--;
                        }

                        if (LeftBlack[pos - 2] > LeftBlack[TripPos])
                        {
                            CommonRectificate(LeftBlack, pos - 2, TripPos);
                            ProcessFlag = 1;
                        }
                        else
                        {
                            for (i = OV7725_EAGLE_W - 1; i > 0; i--)
                            {
                                if (IMG_BUFF[pos - 2, i] == White && IMG_BUFF[pos - 2, i - 1] == Black)
                                {
                                    LeftBlack[pos - 2] = i;
                                    break;
                                }
                            }
                            if (LeftBlack[pos - 2] > LeftBlack[TripPos])
                            {
                                CommonRectificate(LeftBlack, pos - 2, TripPos);
                                ProcessFlag = 1;
                            }
                            else if (NoValidRMax > 25)
                            {
                                LeftBlack[pos - 2] = OV7725_EAGLE_W - 2;
                                CommonRectificate(LeftBlack, pos - 2, TripPos);
                                ProcessFlag = 1;
                            }
                        }
                    }
                }
                else//找不到跳变
                {
                    GetCrossingMidLine();//取边缘线
                    if (g_Derict == L_BlackEnd)//左倾
                    {
                        pos = 20;
                        while (ValidLineR[pos] == 0)
                        {
                            pos++;
                            if (pos >= 59) break;   //2017年3月25日16:24:29修改bug
                        }
                        if (pos < 60)
                            while (ValidLineR[pos] == 1)
                            {
                                pos++;
                                if (pos >= 59) break;
                            }
                        startPos = pos - 2;
                        pos += 8;
                        while (pos < OV7725_EAGLE_H - 1 && (ValidLineR[pos] == 0 || RightBlack[pos] > OV7725_EAGLE_W - 3))
                        {
                            pos++;
                        }
                        endPos = pos + 4;
                        {
                            CommonRectificate(RightBlack, startPos, endPos);
                            ProcessFlag = 1;
                        }
                    }
                    else if (g_Derict == R_BlackEnd)
                    {
                        pos = 20;
                        if (pos < 60)
                            while (ValidLineL[pos] == 0)
                            {
                                pos++;
                                if (pos >= 59) break;
                            }
                        if (pos < 60)
                            while (ValidLineL[pos] == 1)
                            {
                                pos++;
                                if (pos >= 59) break;
                            }
                        startPos = pos - 2;
                        pos += 8;
                        while (pos < OV7725_EAGLE_H - 1 && (ValidLineL[pos] == 0 || LeftBlack[pos] < 3))
                        {
                            pos++;
                        }
                        endPos = pos + 4;
                        {
                            CommonRectificate(LeftBlack, startPos, endPos);
                            ProcessFlag = 1;
                        }
                    }
                }

                if (ProcessFlag == 0)
                {
                    IsCrossing = false;
                    return;
                }
            }
        }

        void UseTemMidLine()
        {
            int i = 0;
            for (i = 0; i < OV7725_EAGLE_H; i++)
            {
                BlackLineData[i] = TemMidLineData[i];//中心线数据转储	
            }
        }


        //新提取十字进行滤波
        void CrossingMidFilter()
        {
            int i = 0, j = 0;
            bool MidEnd = false;
            int MidToBlackCount = 0;
            int sum = 0;
            StableNumbers2 = 0;

            for (i = OV7725_EAGLE_H - 1; i >= 0 && !MidEnd; i--)
            {
                BlackLineData[i] = LeftBlack[i] + (RightBlack[i] - LeftBlack[i]) / 2;
                if (BlackLineData[i] > 79) BlackLineData[i] = 79;
                if (BlackLineData[i] < 0) BlackLineData[i] = 0;
                StableNumbers2++;//十字中心线稳定行计数增加

                if (BlackLineData[i] < 4 || BlackLineData[i] > OV7725_EAGLE_W - 4)
                {
                    MidEnd = true;//中心线搜索停止
                    break;
                }
                if (i < OV7725_EAGLE_H - 5 && ABS(BlackLineData[i] - BlackLineData[i + 1]) > 3)
                {
                    BlackLineData[i] = BlackLineData[i + 1] + BlackLineData[i + 1] - BlackLineData[i + 2];//中心线平均
                }
                if (BlackLineData[i] > 79) BlackLineData[i] = 79;
                if (BlackLineData[i] < 0) BlackLineData[i] = 0;
                if (IMG_BUFF[i, BlackLineData[i]] == Black)
                {
                    ////////////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////
                    //这里需要修改
                    MidToBlackCount++;
                    if (MidToBlackCount >= 3)
                    {
                        //碰到黑点
                        MidEnd = true;
                    }
                }
            }

            for (i = OV7725_EAGLE_H - 1; i > OV7725_EAGLE_H - (StableNumbers2 - 5); i--)
            {
                sum = 0;
                for (j = 0; j < 5; j++)
                {
                    sum += BlackLineData[i - j];
                }
                BlackLineData[i] = sum / 5;
            }
        }

        /******************************************************************************
        //中心线处理
        ******************************************************************************/
        bool StoreFlag = false;

        void MidLineProcess()
        {
            StoreFlag = false;
            GetLMR();//提取左边缘，右边缘，中心线

            //正常情况提取中心线
            if (StableNumbers > 12)
            {
                //LAverageFilter();//左边缘滤波
                //RAverageFilter();//右边缘滤波
                //GetFinalMidLine();//获取舵机中心控制线
                //AverageFilter();//滤波处理
                //MidLineCompensate();//中心线补偿
                StoreFlag = true;
                StoreMidLine();//存储中心线数据
                /*用于赛道检测*/
                //GetSectionParam();//分段提取偏差
                /*用于赛道检测*/
                //GetMidLineVariance();//提取方差
            }
            else
            {
                //GetSpecialError();//获取特殊偏差
            }
            //处理十字
            ProcessCrossing();

            if (!IsCrossing)
            {
                if (StoreFlag)
                {
                    UseTemMidLine();//储存零时中心线
                }
            }
            else
            {
                CrossingMidFilter();//十字中线提取滤波顺滑
                /*用于赛道检测*/
                //GetSectionParam();//分段提取偏差
            }
        }

        const int Size2 = 30;
        int Head2 = 0, Rear2 = 0;

        int StandardRoadType = 1;
        int StraightToBendCount = 0;
        int LastRoadType = 0;
        public int RoadType = -1;
        int[] HistoryRoadType = new int[4];
        int[] RoadTypeData2 = new int[Size2];
        int ElementCount2 = 0;
        int g_Head = 0, g_Rear = 0;

        int AllStraightCount = 0;
        int AllSmallSCount = 0;
        int AllBigSCount = 0;
        int AllBendCount = 0;


        int IsStraightToBend()
        {
            if (ABS(TopE1) > 12 && ABS(TopE2) < 3 && ABS(SubBasePoint) < 3)
            {
                return 1;

            }
            else
            {
                return 0;
            }
        }


        void HistoryRoadTypeCount()
        {
            int i = 0;
            int tem = 0;
            int StraightCount = 0;
            int SmallSCount = 0;
            int BigSCount = 0;
            int BendCount = 0;
            AllStraightCount = 0;
            AllSmallSCount = 0;
            AllBigSCount = 0;
            AllBendCount = 0;
            tem = Rear2;
            for (i = 0; i < ElementCount2; i++)
            {
                if (RoadTypeData2[tem] == 0)
                {
                    StraightCount++;
                    if (AllStraightCount < StraightCount)
                    {
                        AllStraightCount = StraightCount;
                    }
                }
                else
                {
                    StraightCount = 0;
                }

                if (RoadTypeData2[tem] == 0 || RoadTypeData2[tem] == 1)
                {
                    SmallSCount++;
                    if (AllSmallSCount < SmallSCount)
                    {
                        AllSmallSCount = SmallSCount;
                    }
                }
                else
                {
                    SmallSCount = 0;
                }

                if (RoadTypeData2[tem] == 2)
                {
                    BigSCount++;
                    if (AllBigSCount < BigSCount)
                    {
                        AllBigSCount = BigSCount;
                    }
                }
                else
                {
                    BigSCount = 0;
                }

                if (RoadTypeData2[tem] == 2 || RoadTypeData2[tem] == 3)
                {
                    BendCount++;
                    if (AllBendCount < BendCount)
                    {
                        AllBendCount = BendCount;
                    }
                }
                else
                {
                    BendCount = 0;
                }

                tem = (tem - 1 + Size2) % Size2;
            }
        }


        void HistoryRTProccess()
        {

            if ((StandardRoadType != 0) && (RoadType == 0 || RoadType == 1))
            {
                //存入循环队列
                RoadTypeData2[Rear2] = RoadType;
            }
            else
            {
                RoadTypeData2[Rear2] = 2;
            }

            Rear2 = (Rear2 + 1) % Size2;
            ElementCount2++;
            if (ElementCount2 > Size2 - 1)
            {
                ElementCount2 = Size2 - 1;
            }

            //历史赛道类型记录
            HistoryRoadTypeCount();
        }

        //赛道检测
        void RTRecognition()
        {
            int temRoadType = -1;
            //标准赛道类型2,3
            if (ABS(Excursion) > 30)
            {
                if (EPerCount >= 1.2 && BlackEndMax < 15)
                {
                    temRoadType = 3;
                }
                else if (EPerCount >= 1.0 && StableNumbers <= 28 && BlackEndMax < 22)
                {
                    temRoadType = 2;
                }

            }
            //标准赛道类型1,0
            if (BlackEndMax >= OV7725_EAGLE_H - 1)
            {
                if (ABS(Excursion) < 5 && MidLineVariance < 2 && EPerCount < 0.1)
                {
                    temRoadType = 0;
                }
                else if (ABS(Excursion) < 15 && MidLineVariance < 20)
                {
                    temRoadType = 1;
                }
            }
            //非标准赛道
            if (temRoadType == -1)
            {
                StandardRoadType = 0;
                if (BlackEndMax > 33)
                {
                    temRoadType = 1;
                    if (ABS(TopE1 - TopE2) > 27 || Mid_K1 > 1.2 || Mid_K2 > 1.2)
                    {
                        temRoadType = 2;
                        StandardRoadType = 2;
                    }
                    if (LastRoadType == 103)
                    {
                        temRoadType = 103;
                        StraightToBendCount++;
                        if (StraightToBendCount > 2)
                        {
                            temRoadType = 2;
                            StraightToBendCount = 0;
                        }
                    }
                }
                else if (BlackEndMax > 20)
                {
                    temRoadType = 2;
                }
                else
                {
                    temRoadType = 3;
                }
            }
            else
            {
                StandardRoadType = 1;
            }
            RoadType = temRoadType;//赛道判断成功
            HistoryRTProccess();//历史赛道信息
            if (IsStraightToBend() != 0)//直道入弯判断
            {
                RoadType = 103;
            }
            if (RoadType != 103)
            {
                StraightToBendCount = 0;
            }

            HistoryRoadType[g_Rear] = RoadType;

            g_Rear = (g_Rear + 1) % 4;

            if (RoadType != HistoryRoadType[g_Head] && HistoryRoadType[g_Head] == HistoryRoadType[(g_Head + 1) % 4])
            {
                RoadType = HistoryRoadType[g_Head];
            }
            if ((g_Rear + 1) % 4 == g_Head)
            {
                g_Head = (g_Head + 1) % 4;
            }
        }

        //获取中心线偏差量
        float GetSteerError(int start, int end, float midpos)
        {
            int i = 0;
            int iCount = 0;
            int Black_Sum = 0;
            float TemError = 0.0f;
            for (i = start, iCount = 0; i < end; i++, iCount++)
            {
                Black_Sum += BlackLineData[i];
            }
            TemError = Black_Sum * 1.0f / iCount - midpos;
            return TemError;
        }

        int[] LineWeight = new int[OV7725_EAGLE_H];

        float GetSteerError2(int start, int end, float midpos)
        {
            int i = 0;
            //unsigned char iCount=0;
            float Black_Sum = 0;
            int weightSum = 0;
            float TemError = 0.0f;
            MidLineExcursion = 0;
            for (i = 1; i < OV7725_EAGLE_H; i++)
            {
                LineWeight[i] = 1;                             //2017年3月27日18:03:56去掉权重
                Black_Sum += BlackLineData[i] * LineWeight[i];
                weightSum += LineWeight[i];
            }
            TemError = Black_Sum * 1.0f / weightSum - midpos;

            if (TemError > 40.0)
            {
                TemError = 40.0f;
            }
            if (TemError < -40.0f)
            {
                TemError = -40.0f;
            }
            return TemError;
        }

        volatile int IsStartLine = 0;
        int CrossingBegin = 0;
        int CrossingCount = 0;

        int Foresight = 15;//定义前瞻量
        int StraightFS = 45;
        int SmallSFS = 45;
        int BigFS = 40;
        int BendFS = 35;
        int CommonFS = 35;
        int StraightToBendFS = 35;

        int StraightEnd = OV7725_EAGLE_H - 8;
        int SmallSEnd = OV7725_EAGLE_H - 8;
        int BigSEnd = OV7725_EAGLE_H - 6;
        int BendEnd = OV7725_EAGLE_H - 10;
        int CommonEnd = OV7725_EAGLE_H - 8;
        int StraightToBenEnd = OV7725_EAGLE_H - 8;
        int FarWeight = 0;

        float StraightK = 0.3f;
        float SmallSK = 0.40f;
        float BigSK = 0.50f;
        float BendK = 0.65f;
        float CommonK = 0.55f;
        float StraightToBendK = 0.65f;

        float Error = 0.0f;
        float LastError = 0.0f;
        float k = 1.0f;

        // extern float ke = 0.0f;

        void DirectionCtrol()
        {
            float temK = 1.0f;
            int StartPos = OV7725_EAGLE_H - 15;
            int EndPos = OV7725_EAGLE_H - 1;
            int MidPos = (int)(BlackLineData[OV7725_EAGLE_H - 1] - LeftBlack[OV7725_EAGLE_H - 1] * 1.0 / 2 + (OV7725_EAGLE_W - RightBlack[OV7725_EAGLE_H - 1]) * 1.0 / 2);
            int i = 0;
            int CtrlWeight = FarWeight;

            if (StableNumbers < 12)
            {
                if (MidLineExcursion > 0)
                {
                    //			SERVO((int32)SERVO_STEER(35));
                    ke = 35;
                }
                else if (MidLineExcursion < 0)
                {
                    //			SERVO((int32)SERVO_STEER(-35));
                    ke = -35;
                }
                return;
            }

            Error = 0.0f;
            RoadType = 505;                     //2017年3月27日18:02:48去掉赛道检测
            switch (RoadType)
            {
                case 0://直
                    Foresight = StraightFS;
                    if (Foresight > StableNumbers - 10) Foresight = StableNumbers - 10;
                    k = StraightK;
                    StartPos = OV7725_EAGLE_H - Foresight + 1;
                    EndPos = StraightEnd;
                    LastRoadType = RoadType;//
                    break;

                case 1://s
                    k = SmallSK;
                    if (Foresight > StableNumbers - 10) Foresight = StableNumbers - 10;
                    StartPos = OV7725_EAGLE_H - Foresight + 1;
                    EndPos = SmallSEnd;

                    switch (LastRoadType)
                    {
                        case 2:
                            k = k + 0.2f;
                            break;
                        case 3:
                            k = k + 0.3f;
                            break;
                        default:
                            break;
                    }
                    LastRoadType = RoadType;//	
                    break;

                case 2://S
                    Foresight = BigFS;
                    if (Foresight > StableNumbers + CompensateCount - 11) Foresight = StableNumbers + CompensateCount - 11;
                    k = BigSK;
                    StartPos = OV7725_EAGLE_H - Foresight + 1;
                    EndPos = BigSEnd;
                    LastRoadType = RoadType;//
                    break;

                case 3://急弯
                    Foresight = BendFS;//17
                    if (Foresight > StableNumbers + CompensateCount - 11) Foresight = StableNumbers + CompensateCount - 11;
                    k = BendK;
                    StartPos = OV7725_EAGLE_H - Foresight + 1;
                    EndPos = BendEnd;
                    MidPos = OV7725_EAGLE_W / 2 - 1;
                    LastRoadType = RoadType;//			 
                    break;

                case 103://直到进弯
                    Foresight = StraightToBendFS;
                    if (Foresight > StableNumbers + CompensateCount - 11) Foresight = StableNumbers + CompensateCount - 11;
                    StartPos = OV7725_EAGLE_H - Foresight + 1;
                    EndPos = StraightToBenEnd;
                    LastRoadType = 103;
                    k = StraightToBendK;
                    break;

                default:
                    k = CommonK;
                    Foresight = CommonFS;
                    StartPos = OV7725_EAGLE_H - Foresight + 1;
                    EndPos = CommonEnd;
                    break;

            }

            if (IsCrossing)
            {
                Foresight = 20;
                if (Foresight > StableNumbers + CompensateCount - 11)
                    Foresight = StableNumbers + CompensateCount - 11;
                StartPos = OV7725_EAGLE_H - Foresight + 1;
                EndPos = OV7725_EAGLE_H - 5;
            }

            if (StartPos >= EndPos)
            {
                StartPos = EndPos - 2;
                EndPos = EndPos + 2;
            }
            if (IsStartLine != 0)
            {
                StartPos = OV7725_EAGLE_H - 10;
                EndPos = OV7725_EAGLE_H - 1;
                k = 1.0f;
            }


            if (CrossingBegin != 0)
            {
                CtrlWeight = 10;
            }

            for (i = OV7725_EAGLE_H - 1; i > 0; i--)
            {
                if (i > OV7725_EAGLE_H - (StableNumbers + CompensateCount - 11))
                {
                    if (i >= StartPos && i <= EndPos)
                    {
                        if (ValidLine[i] == 0)
                        {
                            LineWeight[i] = 3;
                        }
                        else
                        {
                            LineWeight[i] = 10;
                        }
                    }
                    else if (i > EndPos)
                    {
                        LineWeight[i] = 1;
                    }
                    else if (i > 13)
                    {
                        LineWeight[i] = CtrlWeight;
                    }
                }
                else
                {
                    LineWeight[i] = 0;
                }
            }
            if (Iscircle)
            {
                Error = (R_BlackEnd - L_BlackEnd) * 1.0f;//2017年3月25日17:41:33尝试圆环
                if (Error != 0)
                    ke = Error;
                else
                    ke = 1;
                return;
            }
            if (IsCrossing && CrossingBegin == 0)//十字标志置位，但十字并未开始
            {
                CrossingBegin = 1;//十字开始

                StartPos = OV7725_EAGLE_H - 12;//其实行

                if (StartPos < OV7725_EAGLE_H - (StableNumbers2 - 5))
                {
                    StartPos = OV7725_EAGLE_H - (StableNumbers2 - 5);
                }

                MidPos = OV7725_EAGLE_W / 2 - 1;//改动过，按照我的理解应该是这个

                EndPos = OV7725_EAGLE_H - 1;
                if (EndPos > StartPos)
                    Error = GetSteerError(StartPos, EndPos, MidPos);

                //	SERVO((int32)SERVO_STEER(Error));

                LastError = Error;

                ke = Error;

                return;
            }

            if (CrossingBegin != 0)
            {
                CrossingCount++;//过十字标志进行累加

                if (CrossingCount > 3)//计数大于3
                {
                    CrossingBegin = 0;//开始标志清零
                    CrossingCount = 0;//计数标志清零
                                      //十字走完，结束
                }
                else
                {
                    //	SERVO((int32)SERVO_STEER(LastError));//执行上次舵机动作
                }
            }

            Error = GetSteerError2(StartPos, EndPos, MidPos) * k * temK;//计算误差

            //SERVO((int32)SERVO_STEER(Error));//舵机作用//g

            ke = Error;//把偏差传给电机进行差速

            LastError = Error;//更新	
        }
    }
}
