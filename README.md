- Per utilizzare l'applicazione creare un dbSQL.
- Assicurarsi di cambiare le connection string all'interno dei controllers
(private readonly string _connectionString = "Server=TuaConnectionString; Initial Catalog=TuoNomeDb; Integrated Security=true; TrustServerCertificate=True";)
- Crea e popola le tabelle del db utilizzando le query qui sotto.
----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Creazione della tabella ANAGRAFICA --

CREATE TABLE ANAGRAFICA (
    Idanagrafica INT PRIMARY KEY IDENTITY(1,1),
    Cognome VARCHAR(50),
    Nome VARCHAR(50),
    Indirizzo VARCHAR(100),
    Città VARCHAR(50),
    CAP VARCHAR(10),
    Cod_Fisc VARCHAR(16)
);

-- Creazione della tabella TIPO_VIOLAZIONE --

CREATE TABLE TIPO_VIOLAZIONE (
    Idviolazione INT PRIMARY KEY IDENTITY(1,1),
    descrizione VARCHAR(100)
);

-- Creazione della tabella VERBALE --

CREATE TABLE VERBALE (
    Idverbale INT PRIMARY KEY IDENTITY(1,1),
    DataViolazione DATE,
    IndirizzoViolazione VARCHAR(100),
    NominativoAgente VARCHAR(50),
    DataTrascrizioneVerbale DATE,
    Importo DECIMAL (10, 2),
    DecurtamentoPunti INT,
    Idanagrafica INT,
    Idviolazione INT,
    FOREIGN KEY (Idanagrafica) REFERENCES ANAGRAFICA(Idanagrafica),
    FOREIGN KEY (Idviolazione) REFERENCES TIPO_VIOLAZIONE(Idviolazione)
);
----------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Popolamento della tabella ANAGRAFICA --

INSERT INTO ANAGRAFICA (Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) VALUES
('Rossi', 'Mario', 'Via Roma 1', 'Roma', '00100', 'RSSMRI01A01H501A'),
('Verdi', 'Giuseppe', 'Corso Italia 10', 'Torino', '10100', 'VRDGPP02A02H502B'),
('Bianchi', 'Anna', 'Viale dei Fiori 5', 'Palermo', '90100', 'BNCNNA03A03H503C'),
('Russo', 'Luigi', 'Via Napoli 7', 'Milano', '20100', 'RSSLUI04A04H504D'),
('Ferrari', 'Paola', 'Via Milano 15', 'Firenze', '50100', 'FRRPLA05A05H505E'),
('Esposito', 'Giovanni', 'Via Torino 20', 'Bari', '70100', 'ESPVNI06A06H506F'),
('Romano', 'Francesca', 'Corso Vittorio Emanuele 3', 'Roma', '00100', 'RMMFRA07A07H507G'),
('Gallo', 'Antonio', 'Via Firenze 8', 'Torino', '10100', 'GLLANT08A08H508H'),
('Conti', 'Roberta', 'Piazza Garibaldi 4', 'Palermo', '90100', 'CNTROB09A09H509I'),
('Marino', 'Marco', 'Via Palermo 12', 'Milano', '20100', 'MRNMRC10A10H510L');


-- Popolamento della tabella TIPO_VIOLAZIONE --

INSERT INTO TIPO_VIOLAZIONE (descrizione) VALUES
('Eccesso di velocità'),
('Guida in stato di ebbrezza'),
('Mancato rispetto del segnale di stop'),
('Mancanza di revisione del veicolo'),
('Sosta vietata');

-- Popolamento della tabella VERBALE --

INSERT INTO VERBALE (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, Idanagrafica, Idviolazione) VALUES
('2024-01-15', 'Via Roma 1', 'Agente1', '2024-01-20', 150.00, 3, 7, 4),
('2014-02-05', 'Corso Italia 10', 'Agente2', '2024-02-10', 200.00, 2, 8, 3),
('2009-03-10', 'Viale dei Fiori 5', 'Agente1', '2024-03-15', 100.00, 1, 9, 2),
('2017-04-20', 'Via Napoli 7', 'Agente3', '2024-04-25', 120.00, 2, 10, 5),
('2009-05-05', 'Via Milano 15', 'Agente2', '2024-05-10', 380.00, 4, 1, 1),
('2022-06-15', 'Via Torino 20', 'Agente3', '2024-06-20', 550.00, 6, 2, 4),
('2020-07-25', 'Corso Vittorio Emanuele 3', 'Agente1', '2024-07-30', 1000.00, 2, 3, 2),
('2006-08-10', 'Via Firenze 8', 'Agente2', '2024-08-15', 800.00, 8, 4, 3),
('2024-09-05', 'Piazza Garibaldi 4', 'Agente3', '2024-09-10', 150.00, 3, 5, 4),
('2024-10-20', 'Via Palermo 12', 'Agente1', '2024-10-25', 200.00, 4, 6, 5),
('2010-11-05', 'Via Roma 1', 'Agente2', '2024-11-10', 180.00, 2, 7, 1),
('2024-12-10', 'Corso Italia 10', 'Agente3', '2024-12-15', 220.00, 4, 8, 4),
('2009-01-20', 'Viale dei Fiori 5', 'Agente1', '2024-01-25', 130.00, 3, 9, 3),
('2024-02-25', 'Via Napoli 7', 'Agente2', '2024-02-29', 170.00, 2, 10, 2),
('2009-03-10', 'Via Milano 15', 'Agente3', '2024-03-15', 650.00, 6, 1, 1);
