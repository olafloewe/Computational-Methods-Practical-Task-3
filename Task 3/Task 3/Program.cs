using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Task_3 {
    /*
    double[,] MatrixPad(double[,] A, int m, int n)
        Input:
            two two-dimensional arrays of real numbers
        Output:
            a two-dimensional array, padded with zeros to fit size m by n
        Examples:
            MatrixPad({{1, 2, 3},{4, 5, 6}}, 4, 4)
                    -> {{1, 2, 3, 0}, {4, 5, 6, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}}
            MatrixPad({{1, 2, 3},{4, 5, 6}}, 4, 3)
                    -> {{1, 2, 3}, {4, 5, 6}, {0, 0, 0}, {0, 0, 0}}
            MatrixPad({{1, 2, 3},{4, 5, 6}}, 2, 2)
                    -> Error/Exception (A destination size is smaller than input size)
    */
    private static double[,] MatrixPad(double[,] A, int m, int n) {

        int width = A.GetLength(1);
        int height = A.GetLength(0);

        // invalid size
        if (n < 1 || m < 1) throw new Exception("A destination size is less than 1.");
        if (m < height || n < width) throw new Exception("A destination size is smaller than the input size.");

        // already at given size
        if (height == m && width == n) return A;

        double[,] paddedMatrix = new double[m, n];

        // conditioned copy of matrix
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                paddedMatrix[i, j] = (j < width && i < height)? A[i, j] : 0;
            }
        }

        return paddedMatrix;
    }

    /*
    double[,] MatrixCrop(double[,] A, int m, int n)
        Input:
            two two-dimensional arrays of real numbers
        Output:
            a two-dimensional array, cropped to size m times n
        Examples:
            MatrixCrop({{1, 2, 3, 0}, {4, 5, 6, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}}, 2, 3)
                    -> {{1, 2, 3},{4, 5, 6}}
            MatrixCrop({{1, 2, 3},{4, 5, 6}}, 2, 2)
                    -> {{1, 2}, {4, 5}}
            MatrixCrop({{1, 2, 3},{4, 5, 6}}, 3, 3)
                    -> Error/Exception (A destination size is larger than input size)
    */
    private static double[,] MatrixCrop(double[,] A, int m, int n) {
        int width = A.GetLength(1);
        int height = A.GetLength(0);

        // invalid size
        if (n < 1 || m < 1) throw new Exception("A destination size is less than 1.");
        if (m > height || n > width) throw new Exception("A destination size is greater than the input size.");

        // already at given size
        if (height == m && width == n) return A;

        double[,] croppedMatrix = new double[m, n];

        // conditioned copy of matrix
        for (int i = 0; i < m; i++){
            for (int j = 0; j < n; j++){
                croppedMatrix[i, j] = A[i, j];
            }
        }

        return croppedMatrix;
    }

    /*
    double[,] MatrixScale(double[,] A, double x)
        Input:
            two two-dimensional arrays of real numbers
        Output:
            a two-dimensional array - input matrix, but with each element multiplied by x
        Examples:
            MatrixScale({{1, 2, 3}, {4, 5, 6}}, -1)
                    -> {{-1, -2, -3}, {-4, -5, -6}}
            MatrixScale({{1, 2, 3}, {4, 5, 6}}, 2)
                    -> {{2, 4, 6}, {8, 10, 12}}
    */
    private static double[,] MatrixScale(double[,] A, double x) {
        int width = A.GetLength(1);
        int height = A.GetLength(0);

        // already at given scale
        if (x == 1) return A;

        double[,] scaledMatrix = new double[height, width];

        // multiplication of every cell
        for (int i = 0; i < height; i++){
            for (int j = 0; j < width; j++){
                scaledMatrix[i, j] = (A[i, j] * x);
            }
        }

        return scaledMatrix;
    }

    /*
    double[,] MatrixAdd(double[,] A, double[,] B)
        Input:
            two two-dimensional arrays of real numbers
        Output:
            a two-dimensional array - a sum of input matrices
        Examples:
            MatrixAdd({{1, 2, 3},{4, 5, 6}}, {{7, 8, 9}, {10, 11, 12}})
                    -> {{8, 10, 12}, {14, 16, 18}}
            MatrixAdd({{1, 2, 3},{4, 5, 6}}, {{7, 8}, {9, 10}, {11, 12}})
                    -> Error/Exception (Dimension mismatch)
    */
    private static double[,] MatrixAdd(double[,] A, double[,] B) {
        int widthA = A.GetLength(1);
        int heightA = A.GetLength(0);
        
        // dimensions not matching
        if (widthA != B.GetLength(1) || heightA != B.GetLength(0)) throw new Exception("Matrix dimensions not matching.");

        // addition of B cells to A cells (in situ)
        for (int i = 0; i < heightA; i++){
            for (int j = 0; j < widthA; j++){
                A[i, j] = (A[i, j] + B[i,j]);
            }
        }

        return A;
    }

    /*
    double[,] MatrixMultiply(double[,] A, double[,] B)
        Input:
            two two-dimensional arrays of real numbers
        Output:
            a two-dimensional array - a product of input matrices
        Examples:
            MatrixMultiply({{1, 2, 3},{4, 5, 6}}, {{7, 8}, {9, 10}, {11, 12}})
                    -> {{58, 64}, {139, 154}}
            MatrixMultiply({{1, 2, 3},{4, 5, 6}}, {{7, 8, 9}, {10, 11, 12}})
                    -> Error/Exception (Dimension mismatch)
    */
    private static double[,] MatrixMultiply(double[,] A, double[,] B) {

        int widthA = A.GetLength(1);
        int heightA = A.GetLength(0);
        int widthB = B.GetLength(1);
        int heightB = B.GetLength(0);

        // dimensions not matching
        if (widthA != heightB) throw new Exception("Matrix dimensions not matching.");

        // recursion case
        if (widthA > 2 && heightA > 2) { 
            // do smth
        }

        // base case
        double M1 = ( A[0, 0] + A[2,2] ) * ( B[0,0] + B[1, 1] );
        double M2 = ( A[1,0] + A[1,1] ) * B[0,0];
        double M3 = A[0,0] * ( B[0, 1] - B[1,1] );
        double M4 = A[1,1] * ( B[1,0] - B[0,0] );
        double M5 = ( A[0,0] + A[0,1] ) * B[1,1];
        double M6 = ( A[1,0] - A[0,0] ) * ( B[0,0] + B[0,1] );
        double M7 = ( A[0,1] - A[1,1] ) * ( B[1,0] + B[1,1] );



        return new double[,] { };
    }

    public static void Main(string[] args) {

        // TODO add UI to add matrix and select from operations to execute

        double[,] matrixA = new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 7, 7, 7 }, };
        double[,] matrixB = new double[,] { { 0, 1, 2, 3 }, { 0, 4, 5, 6 }, { 0, 7, 8, 9 }, };

        Console.WriteLine($"Width A: {matrixA.GetLength(1)}");
        Console.WriteLine($"Height A: {matrixA.GetLength(0)}");
        PrintMatrix(matrixA);

        Console.WriteLine($"Width B: {matrixB.GetLength(1)}");
        Console.WriteLine($"Height B: {matrixB.GetLength(0)}");
        PrintMatrix(matrixB);

        PrintMatrix(MatrixPad(matrixA, 4, 4));
        PrintMatrix(MatrixPad(matrixB, 4, 4));

        PrintMatrix(MatrixAdd(MatrixPad(matrixA, 4, 4), MatrixPad(matrixB, 4, 4)));

        Console.ReadKey();
    }

    private static void PrintMatrix(double[,] A) {
        for (int i = 0; i < A.GetLength(0); i++){
            for (int j = 0; j < A.GetLength(1); j++){
                Console.Write(A[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}