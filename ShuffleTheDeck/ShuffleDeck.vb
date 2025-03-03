'ChristopherZ
'Spring 2025
'RCET2265
'Shuffle The Deck
'https://github.com/Christopher-isu/ShuffleTheDeck.git

Option Explicit On 'forces all variables to be declared
Option Strict On 'forces all data types to match

''' <summary>
''' This program simulates a deck of cards. The user can draw a card, shuffle the deck, or quit the program.
''' 
''' Logic:
''' The deck is operates as a 2D array of Booleans and is represented inform of a grid of characters. 
''' The array has 4 rows (one for each suit) and 13 columns (one for each value).
''' The deck is initialized as all undealt cards. When a card is drawn, 
''' the corresponding element in the array is set to True and represented as a solid square character on the grid.
''' </summary>

Module ShuffleDeck
    ''' <summary>
    ''' Declare variables at the module level to make them accessible by all the functions,
    ''' this approach avoids declaring static variables.
    ''' 
    ''' NOTE: It was my choice to do it this way and I know that this is not the best practice for larger programs!
    ''' For a short code like this, it is acceptable to use module-level variables, but for larger programs,
    ''' it is recommended to use classes and objects to encapsulate the state of the deck.
    ''' </summary>

    Dim deck(3, 12) As Boolean ' Boolean array to track if a card has already been dealt
    Dim suits As String() = {"Spades", "Clubs", "Hearts", "Diamonds"} ' Array of suits
    Dim values As String() = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"} ' Array of card values
    Dim rnd As New System.Random() ' Random number generator

    Sub Main()
        ShuffleDeck() ' Initialize the deck as all undealt cards
        System.Console.WriteLine("Press any key to draw a card, 'S' to shuffle the deck, or 'Q' to quit.") ' Prompt user entry

        Do
            Dim input As String = System.Console.ReadLine() ' Read user input
            If input.ToUpper() = "S" Then ' If user inpt is S then shuffle the deck
                ShuffleDeck()
                System.Console.WriteLine("Deck shuffled. Press any key to draw a card, 'S' to shuffle the deck, or 'Q' to quit.")
            ElseIf input.ToUpper() = "Q" Then ' If user input is Q then quit the program
                Exit Do
            Else
                DrawCard() ' Draw a card
                DisplayDeck() ' Display the deck
            End If
        Loop While True ' Loop until user quits

        System.Console.WriteLine("Thanks for playing!") ' Display a goodbye message
    End Sub


    ''' <summary>
    ''' Shuffle the deck by iterating over entire deck array by setting all elements to False.
    ''' </summary>
    Sub ShuffleDeck() ' Shuffle the deck
        For i As Integer = 0 To 3 ' Loop through the suits
            For j As Integer = 0 To 12 ' Loop through the values
                deck(i, j) = False ' Reset the deck
            Next
        Next
    End Sub


    ''' <summary>
    ''' Draw a card from the deck by generating a random suit and value index. 
    ''' The card is marked as dealt in the deck array, and the card is displayed to the user.
    ''' If all cards have been dealt, the deck is shuffled.
    ''' </summary>
    Sub DrawCard()
        Dim suitIndex, valueIndex As Integer    ' Variables to store the suit and value of the card drawn

        Do
            Randomize() 'initialize random number generator
            suitIndex = rnd.Next(0, 4) ' Generate a random suit index
            valueIndex = rnd.Next(0, 13) ' Generate a random value index
        Loop While deck(suitIndex, valueIndex) ' Keep generating until an undealt card is found

        deck(suitIndex, valueIndex) = True ' Mark the card as dealt
        System.Console.WriteLine("Card drawn: " & values(valueIndex) & " of " & suits(suitIndex)) ' Display the card drawn

        If IsDeckEmpty() Then ' Calls the IsDeckEmpty function to check if the deck is empty
            System.Console.WriteLine("All cards have been dealt. Shuffling the deck.") 'Display message
            ShuffleDeck() ' Shuffle the deck
        End If
    End Sub


    ''' <summary>
    ''' Displays the card as a character in a grid. Dealt cards are marked with a filled square character, 
    ''' and undealt cards are marked with white space. Grid is updated row by row.
    ''' </summary>
    Sub DisplayDeck()
        System.Console.WriteLine("|       | Spades  |  Clubs  | Hearts  | Diamonds|")
        System.Console.WriteLine("|-------|---------|---------|---------|---------|")

        For valueIndex As Integer = 0 To 12 ' Loop in range of card values
            Dim row As String = "| " & values(valueIndex).PadRight(5) & " |" ' Padding used to center the card value
            For suitIndex As Integer = 0 To 3   ' Loop through the suits
                If deck(suitIndex, valueIndex) Then ' Check if the card has been dealt
                    row &= "    █    |" ' Mark a card that has been dealt
                Else
                    row &= "         |" ' White space for a card that has not been dealt
                End If
            Next
            System.Console.WriteLine(row) ' Display the row
        Next
    End Sub


    ''' <summary>
    ''' Check if the deck is empty. If all cards have been dealt, shuffle the deck.
    ''' This function is called after each card is drawn - so computationally it is not very efficient,
    ''' but it simplifies the organization of the code compared to implementing tracking of the number of dealt cards
    ''' in the DrawCard function like we did in class working on bingo example.
    ''' </summary>
    Function IsDeckEmpty() As Boolean ' Check if the deck is empty
        For i As Integer = 0 To 3 ' Loop through the suits
            For j As Integer = 0 To 12 ' Loop through the values
                If Not deck(i, j) Then ' If a card has not been dealt
                    Return False
                End If
            Next
        Next
        Return True
    End Function
End Module
