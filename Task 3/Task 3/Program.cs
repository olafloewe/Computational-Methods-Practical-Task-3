using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Task_3{
    private static double[,] MatrixPad(double[,] A, int m, int n){

        int width = A.GetLength(1);
        int height = A.GetLength(0);

        // invalid size
        if (n < 1 || m < 1) throw new Exception("A destination size is less than 1.");
        if (m < height || n < width) throw new Exception("A destination size is smaller than the input size.");

        // already at given size
        if (height == m && width == n) return A;

        double[,] paddedMatrix = new double[m, n];

        // conditioned copy of matrix
        for (int i = 0; i < m; i++){
            for (int j = 0; j < n; j++){
                paddedMatrix[i, j] = (j < width && i < height) ? A[i, j] : 0;
            }
        }

        return paddedMatrix;
    }

    private static List<double[,]> MatrixAutoPad(double[,] A, double[,] B){
        List<double[,]> paddedMatrices = new List<double[,]>();
        int AWidth = A.GetLength(1);
        int AHeight = A.GetLength(0);
        int BWidth = B.GetLength(1);
        int BHeight = B.GetLength(0);

        // largest dimension
        int majorA = (AWidth > AHeight) ? AWidth : AHeight;
        int majorB = (BWidth > BHeight) ? BWidth : BHeight;

        // largest between A and B
        int major = (majorA > majorB) ? majorA : majorB;

        // set major to nearest power of 2
        for (int i = 1; i < int.MaxValue; i *= 2){
            if (major <= i){
                major = i;
                break;
            }
        }

        // padd size to largest needed power of 2
        paddedMatrices.Add(MatrixPad(A, major, major));
        paddedMatrices.Add(MatrixPad(B, major, major));

        return paddedMatrices;
    }

    private static double[,] MatrixCrop(double[,] A, int m, int n, int offsetM = 0, int offsetN = 0){
        int width = A.GetLength(1);
        int height = A.GetLength(0);

        // invalid size
        if (n < 1 || m < 1) throw new Exception("A destination size is less than 1.");
        if (m > height || n > width) throw new Exception("A destination size is greater than the input size.");
        if (m + offsetM > height || n + offsetN > width) throw new Exception("Offset runs out of bounds.");

        // already at given size
        if (height == m && width == n) return A;

        double[,] croppedMatrix = new double[m, n];

        // conditioned copy of matrix
        for (int i = 0; i < m; i++){
            for (int j = 0; j < n; j++){
                croppedMatrix[i, j] = A[i + offsetM, j + offsetN];
            }
        }

        return croppedMatrix;
    }

    private static double[,] MatrixScale(double[,] A, double x){
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

    private static double[,] MatrixAdd(double[,] A, double[,] B){
        int widthA = A.GetLength(1);
        int heightA = A.GetLength(0);

        // dimensions not matching
        if (widthA != B.GetLength(1) || heightA != B.GetLength(0)) throw new Exception("Matrix dimensions not matching.");

        double[,] addedMatrix = new double[heightA, widthA];

        // addition of B cells to A cells (in situ)
        for (int i = 0; i < heightA; i++){
            for (int j = 0; j < widthA; j++){
                addedMatrix[i, j] = (A[i, j] + B[i, j]);
            }
        }

        return addedMatrix;
    }

    private static double[,] MatrixSplice(double[,] A, double[,] B, bool leftToRight = true){
        int widthA = A.GetLength(1);
        int heightA = A.GetLength(0);
        int widthB = B.GetLength(1);
        int heightB = B.GetLength(0);

        double[,] splicedMatrix = (leftToRight) ? new double[heightA, widthA + widthB] : new double[heightA + heightB, widthA];

        // addition of B cells to A cells (in situ)
        for (int i = 0; i < splicedMatrix.GetLength(0); i++){
            for (int j = 0; j < splicedMatrix.GetLength(1); j++){
                if (i < heightA && j < widthA) splicedMatrix[i, j] = A[i, j]; // still in A
                if (leftToRight && j >= widthA && i < heightB) splicedMatrix[i, j] = B[i, j - widthA]; // in B to the right
                if (!leftToRight && i >= heightA && j < widthB) splicedMatrix[i, j] = B[i - heightA, j]; // in B below
            }
        }

        return splicedMatrix;
    }

    // prints matrix
    private static void PrintMatrix(double[,] A){
        for (int i = 0; i < A.GetLength(0); i++){
            for (int j = 0; j < A.GetLength(1); j++){
                Console.Write(A[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private static double[,] MatrixMultiply(double[,] A, double[,] B){
        // useful dimensions
        int widthA = A.GetLength(1);
        int heightA = A.GetLength(0);
        int widthB = B.GetLength(1);
        int heightB = B.GetLength(0);

        // dimensions not matching
        if (widthA != heightB) throw new Exception("Matrix dimensions not matching.");
        if (widthA != widthB && heightA != heightB) throw new Exception("Matrix dimensions is not square.");

        // recursion case
        if (widthA > 2 && heightA > 2){
            int halfScale = widthA / 2;

            // offset to get submatrices
            double[,] A00 = MatrixCrop(A, halfScale, halfScale);
            double[,] A01 = MatrixCrop(A, halfScale, halfScale, 0, halfScale);
            double[,] A10 = MatrixCrop(A, halfScale, halfScale, halfScale, 0);
            double[,] A11 = MatrixCrop(A, halfScale, halfScale, halfScale, halfScale);
            double[,] B00 = MatrixCrop(B, halfScale, halfScale);
            double[,] B01 = MatrixCrop(B, halfScale, halfScale, 0, halfScale);
            double[,] B10 = MatrixCrop(B, halfScale, halfScale, halfScale, 0);
            double[,] B11 = MatrixCrop(B, halfScale, halfScale, halfScale, halfScale);

            // calculate M1 - M7 for submatrices
            double[,] M1 = MatrixMultiply(MatrixAdd(A00, A11), MatrixAdd(B00, B11));
            double[,] M2 = MatrixMultiply(MatrixAdd(A10, A11), B00);
            double[,] M3 = MatrixMultiply(A00, MatrixAdd(B01, MatrixScale(B11, -1)));
            double[,] M4 = MatrixMultiply(A11, MatrixAdd(B10, MatrixScale(B00, -1)));
            double[,] M5 = MatrixMultiply(MatrixAdd(A00, A01), B11);
            double[,] M6 = MatrixMultiply(MatrixAdd(A10, MatrixScale(A00, -1)), MatrixAdd(B00, B01));
            double[,] M7 = MatrixMultiply(MatrixAdd(A01, MatrixScale(A11, -1)), MatrixAdd(B10, B11));

            // reconstruct matrix with M1 - M7
            double[,] M11 = MatrixAdd(M1, MatrixAdd(M4, MatrixAdd(MatrixScale(M5, -1), M7)));
            double[,] M12 = MatrixAdd(M3, M5);
            double[,] M21 = MatrixAdd(M2, M4);
            double[,] M22 = MatrixAdd(M1, MatrixAdd(MatrixScale(M2, -1), MatrixAdd(M3, M6)));

            return MatrixSplice(MatrixSplice(M11, M12, true), MatrixSplice(M21, M22, true), false);
        }

        // base case M1 - M7
        double dM1 = (A[0, 0] + A[1, 1]) * (B[0, 0] + B[1, 1]);
        double dM2 = (A[1, 0] + A[1, 1]) * B[0, 0];
        double dM3 = A[0, 0] * (B[0, 1] - B[1, 1]);
        double dM4 = A[1, 1] * (B[1, 0] - B[0, 0]);
        double dM5 = (A[0, 0] + A[0, 1]) * B[1, 1];
        double dM6 = (A[1, 0] - A[0, 0]) * (B[0, 0] + B[0, 1]);
        double dM7 = (A[0, 1] - A[1, 1]) * (B[1, 0] + B[1, 1]);

        return new double[,] { { dM1 + dM4 - dM5 + dM7, dM3 + dM5 }, { dM2 + dM4, dM1 - dM2 + dM3 + dM6 } };
    }

    public static void Main(string[] args){

        // TODO add UI to add matrix

        double[,] matrixA = new double[,] {
            { 0, 20, 3.14, 1, 8, 55 },
            { 1, 8, 55, 3, 1.31, 8 },
            { 17, 20.7, 3, 0, 7, 21 },
            { 4, 5, 6, 4, 5, 6 }
        };
        double[,] matrixB = new double[,] { 
            { -1, 15, 2, 3 },
            { 50, 4, -8, 6 },
            { 5, 4, 5, 63 },
            { 1.0, -40, 5, 6 },
            { 0, 4, 5.29, 13 },
            { 5, 7, 0, 9 } 
        };

        int AWidth = matrixA.GetLength(1);
        int AHeight = matrixA.GetLength(0);
        int BWidth = matrixB.GetLength(1);
        int BHeight = matrixB.GetLength(0);

        Console.WriteLine($"A: Width:{AWidth} Height:{AHeight}\n");
        PrintMatrix(matrixA);

        Console.WriteLine($"B: Width:{BWidth} Height:{BHeight}\n");
        PrintMatrix(matrixB);

        if (AWidth != BHeight) throw new Exception("Matrix dimensions not matching for multiplication.");

        List<double[,]> matrices = MatrixAutoPad(matrixA, matrixB);
        matrixA = matrices[0];
        matrixB = matrices[1];

        // calculation and cropping to match input matrix sizes
        double[,] result = MatrixMultiply(matrixA, matrixB); // could be done in one line ( double[,] result = MatrixCrop(MatrixMultiply(matrixA, matrixB), AHeight, BWidth); )
        result = MatrixCrop(result, AHeight, BWidth);

        Console.WriteLine("Result of AxB: \n");
        PrintMatrix(result);

        // HOLD THE LINE (terminal) !!! 
        Console.ReadKey();
    }
}