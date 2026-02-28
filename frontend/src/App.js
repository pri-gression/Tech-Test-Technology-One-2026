import { useState } from 'react';
import './App.css';

function App(){
  const [number, setNumber] = useState('');
  const [result, setResult] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleConvert = async () => {
    setError('');
    setResult('');
    setLoading(true);

    try {
      const response = await fetch(`http://localhost:5161/api/convert?number=${number}`);
      const data = await response.json();

      if (data.success) {
        setResult(data.result);
      } else {
        setError(data.error); 
      }
    } catch (err) {
      setError('Could not connect to the server!'); 
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className = "container">
      <h1> Number to Words </h1>
      <input type = "text" value = {number}  onChange={(e) => setNumber(e.target.value)} placeholder="Enter amount (e.g. 123.45)" />
      <button onClick={handleConvert} disabled={loading}>
        {loading ? 'Converting...' : 'Convert'}
      </button>
      {result && <p className="result">{result}</p>}
      {error && <p className="error">{error}</p>}
    </div>
  );
}

export default App; 