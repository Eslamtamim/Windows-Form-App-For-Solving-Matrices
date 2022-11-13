using System.Drawing.Drawing2D;

namespace AllgebraTaskWinForm
{
    public partial class Form1 : Form
    {

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
                    if (j != r)
                    {
                        float sub = matrix[j, lead];
                        for (int k = 0; k < columnCount; k++)
                        {
                            matrix[j, k] -= (sub * matrix[r, k]);
                        }

                        //A comment to clear out what happend in this step
                        this.richTextBox1.Text += $"Eliminate the column number {r + 1} of the row number {j + 1} \n";
                        
                        //loop the matrix to print the new changes made to it
                        loop(matrix);
                    }
                }
                lead++;
            }
            return matrix;
        }


        //some test cases to try
        public float[,] matrix = new float[3, 4]
        {
                {  1   , 2  , -1 , -4 },
                {  2   , 3  , -1 , -11},
                { -2   , 0  , -3 ,  22},
        };

        float[,] matrix2 = new float[2, 6]{
                {  1, 1, 1, 1,1 ,2},
                {  1, 1, 1, 0,0,4 }
        };


        private void button1_Click(object sender, EventArgs e)
        {
            //for clearing out the textbox to use it again
            this.richTextBox6.Text = string.Empty;

            //getting the number of the rows and columns from the user (r = rows , c = columns)
            int r = int.Parse(this.richTextBox4.Text);
            int c = int.Parse(this.richTextBox5.Text);

            //some important vars for the matrix that will be used most of the time
            string theEquationsInOneString = this.richTextBox2.Text;           
            string[] rowOfStrings = theEquationsInOneString.Split("/v");
            string[] theTempArray = new string[r * c];
            string theCurrentCellInTheTempArray = string.Empty;
          

            for (int i = 0; i < theTempArray.Length; i++)
            {
                for(int l = 0; l < rowOfStrings.Length; l++)
                {
                    for (int j = 0; j < rowOfStrings[l].Length; j++)
                    {
                        string theCurrentChar = Convert.ToString(rowOfStrings[l][j]);
                        if (theCurrentChar == "+"|| theCurrentChar == "-")
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
                            rowOfStrings[l] = rowOfStrings[l].Substring(j + 1);
                            theCurrentCellInTheTempArray = string.Empty;
                            break;
                        }
                    }
                }
            }

            float[,] inputmatrix = new float[r, c];
            foreach (var item in theTempArray)
            {
                for (int i = 0, u = 0; i < inputmatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < inputmatrix.GetLength(1); j++)
                    {
                        inputmatrix[i, j] =int.Parse(theTempArray[u]);
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
                    if (j == 3) { this.richTextBox6.Text += "]"; }
                    if (j < 3) { this.richTextBox6.Text += ", "; }
                }
                this.richTextBox6.Text += "\n";
            }
            this.richTextBox6.Text += "\n";


            //calling the method to solve the matrix
            this.matrix = ToReducedRowEchelonForm(inputmatrix);



            //****this section is still in progress*****

            /*for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    this.richTextBox3.Text += "X = " + inputmatrix[0, c - 1] + "\n";
                    this.richTextBox3.Text += "Y = " + inputmatrix[1, c - 1] + "\n";
                    this.richTextBox3.Text += "Z = " + inputmatrix[2, c - 1] + "\n";
                }
            }*/

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