// const express = require('express');
// const router = express.Router();
// const db = require('./db'); // Assumes you have a db module for SQL operations

// // Helper function to execute SQL queries
// const executeQuery = (query, params = []) => {
//   return new Promise((resolve, reject) => {
//     db.query(query, params, (err, result) => {
//       if (err) reject(err);
//       else resolve(result);
//     });
//   });
// };

// // Lookup Table
// router.get('/lookups', async (req, res) => {
//   try {
//     const result = await executeQuery('SELECT * FROM Lookup_table');
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.post('/lookups', async (req, res) => {
//   const { Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active } = req.body;
//   try {
//     const result = await executeQuery(
//       'INSERT INTO Lookup_table (Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active) VALUES (?, ?, ?, ?)',
//       [Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.put('/lookups/:id', async (req, res) => {
//   const { id } = req.params;
//   const { Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active } = req.body;
//   try {
//     const result = await executeQuery(
//       'UPDATE Lookup_table SET Lookup_Code = ?, Lookup_Type = ?, Lookup_Desc = ?, Is_Active = ? WHERE Lookup_Id = ?',
//       [Lookup_Code, Lookup_Type, Lookup_Desc, Is_Active, id]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.delete('/lookups/:id', async (req, res) => {
//   const { id } = req.params;
//   try {
//     const result = await executeQuery('DELETE FROM Lookup_table WHERE Lookup_Id = ?', [id]);
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// // Account Table
// router.get('/accounts', async (req, res) => {
//   try {
//     const result = await executeQuery('SELECT * FROM Account_table');
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.post('/accounts', async (req, res) => {
//   const { AccountNumber, AccountStatus_id } = req.body;
//   try {
//     const result = await executeQuery(
//       'INSERT INTO Account_table (AccountNumber, AccountStatus_id) VALUES (?, ?)',
//       [AccountNumber, AccountStatus_id]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.put('/accounts/:id', async (req, res) => {
//   const { id } = req.params;
//   const { AccountNumber, AccountStatus_id } = req.body;
//   try {
//     const result = await executeQuery(
//       'UPDATE Account_table SET AccountNumber = ?, AccountStatus_id = ? WHERE AccId = ?',
//       [AccountNumber, AccountStatus_id, id]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.delete('/accounts/:id', async (req, res) => {
//   const { id } = req.params;
//   try {
//     const result = await executeQuery('DELETE FROM Account_table WHERE AccId = ?', [id]);
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// // Address Table
// router.get('/addresses', async (req, res) => {
//   try {
//     const result = await executeQuery('SELECT * FROM Address_table');
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.post('/addresses', async (req, res) => {
//   const { AccountID, AddressTypeID, Address } = req.body;
//   try {
//     const result = await executeQuery(
//       'INSERT INTO Address_table (AccountID, AddressTypeID, Address) VALUES (?, ?, ?)',
//       [AccountID, AddressTypeID, Address]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.put('/addresses/:id', async (req, res) => {
//   const { id } = req.params;
//   const { AccountID, AddressTypeID, Address } = req.body;
//   try {
//     const result = await executeQuery(
//       'UPDATE Address_table SET AccountID = ?, AddressTypeID = ?, Address = ? WHERE AddressID = ?',
//       [AccountID, AddressTypeID, Address, id]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.delete('/addresses/:id', async (req, res) => {
//   const { id } = req.params;
//   try {
//     const result = await executeQuery('DELETE FROM Address_table WHERE AddressID = ?', [id]);
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// // Account Holder Table
// router.get('/account_holders', async (req, res) => {
//   try {
//     const result = await executeQuery('SELECT * FROM Account_holder_table');
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.post('/account_holders', async (req, res) => {
//   const { AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD } = req.body;
//   try {
//     const result = await executeQuery(
//       'INSERT INTO Account_holder_table (AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD) VALUES (?, ?, ?, ?, ?)',
//       [AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.put('/account_holders/:id', async (req, res) => {
//   const { id } = req.params;
//   const { AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD } = req.body;
//   try {
//     const result = await executeQuery(
//       'UPDATE Account_holder_table SET AccNum = ?, AccTypeID = ?, Acc_holders_N = ?, AC_Balance = ?, CD = ? WHERE AccountHolderID = ?',
//       [AccNum, AccTypeID, Acc_holders_N, AC_Balance, CD, id]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.delete('/account_holders/:id', async (req, res) => {
//   const { id } = req.params;
//   try {
//     const result = await executeQuery('DELETE FROM Account_holder_table WHERE AccountHolderID = ?', [id]);
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// // Transaction Table
// router.get('/transactions', async (req, res) => {
//   try {
//     const result = await executeQuery('SELECT * FROM Transaction_table');
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.post('/transactions', async (req, res) => {
//   const { AccountID, TransAmount, TransTypeID, TransDate } = req.body;
//   try {
//     const result = await executeQuery(
//       'INSERT INTO Transaction_table (AccountID, TransAmount, TransTypeID, TransDate) VALUES (?, ?, ?, ?)',
//       [AccountID, TransAmount, TransTypeID, TransDate]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.put('/transactions/:id', async (req, res) => {
//   const { id } = req.params;
//   const { AccountID, TransAmount, TransTypeID, TransDate } = req.body;
//   try {
//     const result = await executeQuery(
//       'UPDATE Transaction_table SET AccountID = ?, TransAmount = ?, TransTypeID = ?, TransDate = ? WHERE TransactionID = ?',
//       [AccountID, TransAmount, TransTypeID, TransDate, id]
//     );
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// router.delete('/transactions/:id', async (req, res) => {
//   const { id } = req.params;
//   try {
//     const result = await executeQuery('DELETE FROM Transaction_table WHERE TransactionID = ?', [id]);
//     res.json(result);
//   } catch (error) {
//     res.status(500).json({ error: error.message });
//   }
// });

// module.exports = router;
