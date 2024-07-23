const express = require('express');
const sql = require('mssql/msnodesqlv8'); // Import the mssql package for SQL Server
const cors = require('cors');
const app = express();
const port = 3000; // Adjusted port number if needed

app.use(cors());
app.use(express.json());

// MSSQL configuration
const config = {
  server: 'localhost\\SQLEXPRESS', // Adjust server name if necessary
  database: 'VuejsApp',
  driver: 'msnodesqlv8',
  options: {
    trustServerCertificate: true,
    trustedConnection: true,
    enableArithAbort: true,
    integratedSecurity: true, // Use integrated security (Windows Authentication)
  },
};

// Connect to the database
sql.connect(config, err => {
  if (err) {
    console.error('Database connection error:', err);
    process.exit(1);
  }
  console.log('Database connected...');
});

// Helper function to execute SQL queries
const executeQuery = async (query, params = []) => {
  try {
    const pool = await sql.connect(config);
    const result = await pool.request()
      .query(query);
    return result.recordset;
  } catch (err) {
    throw err;
  }
};

// Lookup Table
app.get('/lookups', async (req, res) => {
  try {
    const result = await executeQuery('SELECT * FROM Lookup_table');
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.post('/lookups', async (req, res) => {
  const { Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'INSERT INTO Lookup_table (Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active) VALUES (@Lookup_Code, @Lookup_Type, @Lookup_Desc, @Is_Active)',
      {
        Lookup_Code,
        Lookup_Type,
        Lookup_Desc,
        Is_Active,
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.put('/lookups/:id', async (req, res) => {
  const { id } = req.params;
  const { Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'UPDATE Lookup_table SET Lookup_Code = @Lookup_Code, Lookup_Type = @Lookup_Type, Lookup_Desc = @Lookup_Desc, Is_Active = @Is_Active WHERE Lookup_Id = @id',
      {
        Lookup_Code,
        Lookup_Type,
        Lookup_Desc,
        Is_Active,
        id
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.delete('/lookups/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'DELETE FROM Lookup_table WHERE Lookup_Id = @id',
      { id }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Account Table
app.get('/accounts', async (req, res) => {
  try {
    const result = await executeQuery('SELECT * FROM Account_table');
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.post('/accounts', async (req, res) => {
  const { AccountNumber, AccountStatus_id } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'INSERT INTO Account_table (AccountNumber, AccountStatus_id) VALUES (@AccountNumber, @AccountStatus_id)',
      {
        AccountNumber,
        AccountStatus_id
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.put('/accounts/:id', async (req, res) => {
  const { id } = req.params;
  const { AccountNumber, AccountStatus_id } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'UPDATE Account_table SET AccountNumber = @AccountNumber, AccountStatus_id = @AccountStatus_id WHERE AccId = @id',
      {
        AccountNumber,
        AccountStatus_id,
        id
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.delete('/accounts/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'DELETE FROM Account_table WHERE AccId = @id',
      { id }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Address Table
app.get('/addresses', async (req, res) => {
  try {
    const result = await executeQuery('SELECT * FROM Address_table');
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.post('/addresses', async (req, res) => {
  const { AccountID, AddressTypeID, Address } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'INSERT INTO Address_table (AccountID, AddressTypeID, Address) VALUES (@AccountID, @AddressTypeID, @Address)',
      {
        AccountID,
        AddressTypeID,
        Address
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.put('/addresses/:id', async (req, res) => {
  const { id } = req.params;
  const { AccountID, AddressTypeID, Address } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'UPDATE Address_table SET AccountID = @AccountID, AddressTypeID = @AddressTypeID, Address = @Address WHERE AddressID = @id',
      {
        AccountID,
        AddressTypeID,
        Address,
        id
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.delete('/addresses/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'DELETE FROM Address_table WHERE AddressID = @id',
      { id }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Account Holder Table
app.get('/account_holders', async (req, res) => {
  try {
    const result = await executeQuery('SELECT * FROM Account_holder_table');
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.post('/account_holders', async (req, res) => {
  const { AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'INSERT INTO Account_holder_table (AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD) VALUES (@AccNum, @AccTypeID, @Acc_holders_N, @AC_Balance, @CD)',
      {
        AccNum,
        AccTypeID,
        Acc_holders_N,
        AC_Balance,
        CD
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.put('/account_holders/:id', async (req, res) => {
  const { id } = req.params;
  const { AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'UPDATE Account_holder_table SET AccNum = @AccNum, AccTypeID = @AccTypeID, Acc_holders_N = @Acc_holders_N, AC_Balance = @AC_Balance, CD = @CD WHERE AccountHolderID = @id',
      {
        AccNum,
        AccTypeID,
        Acc_holders_N,
        AC_Balance,
        CD,
        id
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.delete('/account_holders/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'DELETE FROM Account_holder_table WHERE AccountHolderID = @id',
      { id }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Transaction Table
app.get('/transactions', async (req, res) => {
  try {
    const result = await executeQuery('SELECT * FROM Transaction_table');
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.post('/transactions', async (req, res) => {
  const { AccountID, TransAmount, TransTypeID, TransDate } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'INSERT INTO Transaction_table (AccountID, TransAmount, TransTypeID, TransDate) VALUES (@AccountID, @TransAmount, @TransTypeID, @TransDate)',
      {
        AccountID,
        TransAmount,
        TransTypeID,
        TransDate
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.put('/transactions/:id', async (req, res) => {
  const { id } = req.params;
  const { AccountID, TransAmount, TransTypeID, TransDate } = req.body;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'UPDATE Transaction_table SET AccountID = @AccountID, TransAmount = @TransAmount, TransTypeID = @TransTypeID, TransDate = @TransDate WHERE TransactionID = @id',
      {
        AccountID,
        TransAmount,
        TransTypeID,
        TransDate,
        id
      }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

app.delete('/transactions/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await sql.connect(config);
    const result = await sql.query(
      'DELETE FROM Transaction_table WHERE TransactionID = @id',
      { id }
    );
    res.json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

// Start the server
const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
