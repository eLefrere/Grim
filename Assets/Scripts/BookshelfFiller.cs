using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BookshelfFiller : MonoBehaviour
{
    public Transform bookRows;
    public int rowsCount;

    public int maxBooksPerRow;
    public int minBooksPerRow;

    public int totalBooksAmount;

    public float bookWidth;
    public float bookHeight;

    public GameObject[] bookPrefabs;
    public GameObject[] books;

    public GameObject[] puzzleBookPrefabs;

    public bool hasPuzzleBooks;


    public bool isFilled;

    private void Start()
    {
        if (bookRows == null)
            return;

        if (rowsCount != bookRows.childCount)
            rowsCount = bookRows.childCount;

        if (books.IsEmpty() || books[0] == null)
            books = new GameObject[(rowsCount + 1) * maxBooksPerRow];

        if (!isFilled && books[0] == null)
        {                      
            isFilled = true;
            FillBookRows();
        }

        if (hasPuzzleBooks)
            SwapRandomsToPuzzleBooks();
      
    }

    public void FillBookRows()
    {
        int bookIndex = 0;
        
        for (int i = 0; i < rowsCount; i++)
        {
            Transform row = bookRows.GetChild(i);
            int randomBookAmount = Random.Range(minBooksPerRow, maxBooksPerRow);
            Vector3 bookPos = new Vector3(row.position.x, row.position.y, row.position.z);

            for (int j = 0; j < randomBookAmount; j++)
            {

                Vector3 nextBookPos = new Vector3(bookPos.x, bookPos.y, bookPos.z - bookWidth * j);

                if (j == 0)
                {
                    books[bookIndex] = Instantiate(bookPrefabs[Random.Range(0, bookPrefabs.Length)], bookPos, row.rotation);
                }
                if(j > 0 && j <= randomBookAmount)
                {
                    books[bookIndex] = Instantiate(bookPrefabs[Random.Range(0, bookPrefabs.Length)], nextBookPos, row.rotation);
                }
                books[bookIndex].transform.SetParent(row);
                bookIndex++;
            }
          
        }

        totalBooksAmount = bookIndex + 1;
    }

    public void SwapRandomsToPuzzleBooks()
    {
        int[] fourRandomSlots = new int[4];

        for (int i = 0; i < fourRandomSlots.Length; i++)
        {
            fourRandomSlots[i] = Random.Range(0, totalBooksAmount);
        }

        for (int i = 0; i < books.Length; i++)
        {
            if(books[i] != null)
            {
                Vector3 pos = books[i].transform.position;
                Quaternion rot = books[i].transform.rotation;
                Transform parent = books[i].transform.parent;

                for (int j = 0; j < fourRandomSlots.Length; j++)
                {
                    if( i == fourRandomSlots[j])
                    {
                        DestroyImmediate(books[i]);
                        books[i] = null;
                        books[i] = Instantiate(puzzleBookPrefabs[j], pos, rot);
                        books[i].transform.parent = parent;
                    }
                }
            }
        }
    }
}
