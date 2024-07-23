const express = require('express');
const sql = require('mssql/msnodesqlv8');
const cors = require('cors');
const app = express();
const port = 3081; // Adjusted port number if needed

app.use(cors());
app.use(express.json());

// MSSQL configuration
const config = {
    server: 'localhost\\SQLEXPRESS', // Adjust server name if necessary
    database: 'WebApp',
    driver: 'msnodesqlv8',
    options: {
        trustServerCertificate: true,
        trustedConnection: true,
        enableArithAbort: true,
        integratedSecurity: true, // Use integrated security (Windows Authentication)
    },
};
sql.connect(config).then(pool => {
    if (pool.connected) {
        console.log('Connected to MSSQL');
    }

    // CRUD routes
    app.get('/lookups', async (req, res) => {
        try {
            const result = await pool.request().query('SELECT * FROM Lookups');
            res.json(result.recordset);
        } catch (err) {
            res.status(500).send(err.message);
        }
    });

    app.post('/lookups', async (req, res) => {
        const { Lookup_id, Lookup_code, Lookup_Type, Lookup_Desc, Is_Active } = req.body;
        try {
            await pool.request()
                .input('Lookup_id', sql.Int, Lookup_id)
                .input('Lookup_code', sql.Int, Lookup_code)
                .input('Lookup_Type', sql.VarChar(25), Lookup_Type)
                .input('Lookup_Desc', sql.VarChar(25), Lookup_Desc)
                .input('Is_Active', sql.VarChar(10), Is_Active)
                .query('INSERT INTO Lookups (Lookup_id, Lookup_code, Lookup_Type, Lookup_Desc, Is_Active) VALUES (@Lookup_id, @Lookup_code, @Lookup_Type, @Lookup_Desc, @Is_Active)');
            res.status(201).send('Lookup created');
        } catch (err) {
            res.status(500).send(err.message);
        }
    });

    app.put('/lookups/:id', async (req, res) => {
        const { id } = req.params;
        const { Lookup_code, Lookup_Type, Lookup_Desc, Is_Active } = req.body;
        try {
            await pool.request()
                .input('Lookup_id', sql.Int, id)
                .input('Lookup_code', sql.Int, Lookup_code)
                .input('Lookup_Type', sql.VarChar(25), Lookup_Type)
                .input('Lookup_Desc', sql.VarChar(25), Lookup_Desc)
                .input('Is_Active', sql.VarChar(10), Is_Active)
                .query('UPDATE Lookups SET Lookup_code = @Lookup_code, Lookup_Type = @Lookup_Type, Lookup_Desc = @Lookup_Desc, Is_Active = @Is_Active WHERE Lookup_id = @Lookup_id');
            res.send('Lookup updated');
        } catch (err) {
            res.status(500).send(err.message);
        }
    });

    app.delete('/lookups/:id', async (req, res) => {
        const { id } = req.params;
        try {
            await pool.request()
                .input('Lookup_id', sql.Int, id)
                .query('DELETE FROM Lookups WHERE Lookup_id = @Lookup_id');
            res.send('Lookup deleted');
        } catch (err) {
            res.status(500).send(err.message);
        }
    });

}).catch(err => {
    console.error('Database connection failed:', err);
});

app.listen(port, () => {
    console.log(`Server running at http://localhost:${port}`);
});
