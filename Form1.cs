using System.Drawing.Drawing2D;

namespace AllgebraTaskWinForm
{
    public partial class Form1 : Form
    {
        bool btn;
        float[,] ToReducedRowEchelonForm(float[,] matrix)
        {
            //some important vars for the matrix that will be used most of the time
            int lead = 0, rowCount = matrix.GetLength(0), columnCount = matrix.GetLength(1);

            // the method that loops the matrix every time it changes
            void loop(float[,] matrix)
            {
                this.richTextBox1.ForeColor = Color.Red;

                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    double eps = 1e-6;
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (j == 0) { this.richTextBox1.Text += "["; }

                        if (Math.Abs(matrix[k, j]) <= eps) matrix[k, j] = 0;
                        this.richTextBox1.Text += matrix[k, j];

                        if (j == columnCount - 1) { this.richTextBox1.Text += "]"; }
                        if (j < columnCount - 1) { this.richTextBox1.Text += ", "; }
                    }
                    this.richTextBox1.Text += "\n";
                }
                this.richTextBox1.Text += "\n";
            }

            //getting in the matrix and doing the steps to solve it
            for (int r = 0; r < rowCount; r++)
            {

                if (columnCount <= lead)
                {
                    //loop the matrix to print the new changes made to it
                    loop(matrix);
                    this.richTextBox1.Text += "";
                    return matrix;
                }
                int i = r;
                while (matrix[i, lead] == 0)
                {
                    i++;
                    if (rowCount == i)
                    {
                        i = r;
                        lead++;
                        //loop the matrix to print the new changes made to it
                        loop(matrix);
                        if (columnCount == lead)
                        {
                            //loop the matrix to print the new changes made to it
                            loop(matrix);
                            return matrix;
                        }
                    }
                }

                if (i != r)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        float temp = matrix[r, j];
                        matrix[r, j] = matrix[i, j];
                        matrix[i, j] = temp;
                        //loop the matrix to print the new changes made to it
                        loop(matrix);
                    }
                    //loop the matrix to print the new changes made to it
                    loop(matrix);
                }

                float div = matrix[r, lead];
                if (div != 0) for (int j = 0; j < columnCount; j++) matrix[r, j] /= div;

                //A comment to clear out what happend in this step
                this.richTextBox1.Text += $"Find the pivot in the column number {r + 1} in the row number {r + 1} \n";

                //loop the matrix to print the new changes made to it
                loop(matrix);

                for (int j = 0; j < rowCount; j++)
                {

                    if (btn)
                    {

                    if (j > r)
                    {
                        float sub = matrix[j, lead];
                        //A comment to clear out what happend in this step
                     
                        this.richTextBox1.Text += $"Subtract {matrix[j,r]} times (Row {r+1}) to (Row {j+1}) such as the element in row {j+1}, column {r+1} becomes 0:\n\n" ;
                        for (int k = 0; k < columnCount; k++)
                        {
                            matrix[j, k] -= (sub * matrix[r, k]);
                        }   
                        //loop the matrix to print the new changes made to it
                        loop(matrix);
                    }
                    }
                    else
                    {
                        if (j != r)
                        {
                            float sub = matrix[j, lead];
                            //A comment to clear out what happend in this step

                            this.richTextBox1.Text += $"Subtract {matrix[j, r]} times (Row {r + 1}) to (Row {j + 1}) such as the element in row {j + 1}, column {r + 1} becomes 0:\n\n";
                            for (int k = 0; k < columnCount; k++)
                            {
                                matrix[j, k] -= (sub * matrix[r, k]);
                            }
                            //loop the matrix to print the new changes made to it
                            loop(matrix);
                        }
                    }

                }
                lead++;
            }
            return matrix;
        }


        //****some test cases to try***

        /*public float[,] matrix = new float[3, 4]
        {
                {  1   , 2  , -1 , -4 },
                {  2   , 3  , -1 , -11},
                { -2   , 0  , -3 ,  22},
        };

        float[,] matrix2 = new float[2, 6]{
                {  1, 1, 1, 1,1 ,2},
                {  1, 1, 1, 0,0,4 }
        };*/


        float[,] inputmatrix;
        private void stringInput()
        {
            //getting the number of the rows and columns from the user (r = rows , c = columns)
            int r = int.Parse(this.richTextBox4.Text);
            int c = int.Parse(this.richTextBox5.Text);

            //some important vars for the matrix that will be used most of the time

            string theEquationsInOneString = this.richTextBox2.Text;           
            string[] rowOfStrings = theEquationsInOneString.Split("/v");
            string[] theTempArray = new string[r * c];
            char[] vars = new char[r * c];
            string theCurrentCellInTheTempArray = string.Empty;

            for (int i = 0; i < theTempArray.Length; i++)
            {
                for (int l = 0; l < rowOfStrings.Length; l++)
                {
                    for (int j = 0; j < rowOfStrings[l].Length; j++)
                    {
                        string theCurrentChar = Convert.ToString(rowOfStrings[l][j]);

                        if (theCurrentChar == "+" || theCurrentChar == "-")
                        {
                            theCurrentCellInTheTempArray += theCurrentChar;
                            continue;
                        }
                        else if (theCurrentChar == "=" || theCurrentChar == " ")
                        {
                            continue;
                        }
                        if ((int.TryParse(theCurrentChar, out int x)))
                        {
                            theCurrentCellInTheTempArray += x.ToString();
                            theTempArray[i] = theCurrentCellInTheTempArray;
                        }
                        else
                        {
                            vars[i] = Convert.ToChar(theCurrentChar);
                            rowOfStrings[l] = rowOfStrings[l].Substring(j + 1);
                            theCurrentCellInTheTempArray = string.Empty;
                            break;
                        }
                    }
                }
            }


            this.inputmatrix = new float[r, c];
            foreach (var item in theTempArray)
            {
                for (int i = 0, u = 0; i < inputmatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < inputmatrix.GetLength(1); j++)
                    {
                        inputmatrix[i, j] = int.Parse(theTempArray[u]);
                        u++;
                    }
                }
            }

            //just looping the input array to display the augmented matrix

            for (int k = 0; k < inputmatrix.GetLength(0); k++)
            {
                double eps = 1e-6;
                for (int j = 0; j < inputmatrix.GetLength(1); j++)
                {
                    if (j == 0) { this.richTextBox6.Text += "["; }

                    if (Math.Abs(inputmatrix[k, j]) <= eps) inputmatrix[k, j] = 0;
                    this.richTextBox6.Text += inputmatrix[k, j];

                    if (j == c - 1) { this.richTextBox6.Text += "]"; }
                    if (j < c - 1) { this.richTextBox6.Text += ", "; }
                }
                this.richTextBox6.Text += "\n";
            }
            this.richTextBox6.Text += "\n";


            //calling the method to solve the matrix 
            ToReducedRowEchelonForm(this.inputmatrix);

            //****this section is still in progress*****

            if (!btn)
            {
                for (int i = 0; i < r; i++)
                {
                    var temp = "";
                    for (int j = 0; j < c; j++)
                    {
                        var f = $"{vars[i]} = " + this.inputmatrix[i, c - 1] + "\n";
                        if (temp == f) break;
                        this.richTextBox3.Text += f;
                        temp = f;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //for clearing out the textbox to use it again
            this.richTextBox6.Text = string.Empty;
            this.richTextBox1.Text = string.Empty;
            this.richTextBox3.Text = string.Empty;

            btn = false;

            //calling the method that does all the operations to the input text to extract the values out of it
            stringInput();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //for clearing out the textbox to use it again
            this.richTextBox6.Text = string.Empty;
            this.richTextBox1.Text = string.Empty;
            this.richTextBox3.Text = string.Empty;

            btn = true;
            //calling the method that does all the operations to the input text to extract the values out of it
            stringInput();

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            string r = richTextBox4.Text;
        }

        private void richTextBox5_TextChanged(object sender, EventArgs e)
        {

            string c = richTextBox4.Text;

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

    }

}
/*
1x + 1y + 1z = 4
 2x + 1y + 0z = 2
3x - 1y + 1z = 6*/
