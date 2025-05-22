import { useEffect, useState } from 'react';
import './App.css';
import CoworkerBox from './CoworkerBox';
import AddCoworkerModal from './AddCoworkerModal';

function App() {
    const [coworkerTabs, setCoworkerTabs] = useState();
    const [revision, setRevision] = useState();
    const [showAddCoworker, setShowAddCoworker] = useState();

    useEffect(() => {
        fetchRunningTabs();
    }, []);

    const refreshTabs = () => {
        fetchRunningTabs();
    }

    const loading = <div>Loading...</div>;

    const content = coworkerTabs === undefined ? loading :
        <div>
            {coworkerTabs.map(coworkerTab => <CoworkerBox name={coworkerTab.coworkerName} isNext={coworkerTab.isNextToPay} runningTab={coworkerTab.runningTab} />)}
        </div>;

    const onAddCoworker = (value) => {
        setShowAddCoworker(value);
    }


    return (
        <div>
            <h1 id="mainheader">It's Coffee Time!</h1>
            <div>
                <button onClick={() => onAddCoworker(true)}>Add Coworker</button>
                <button>Remove Coworker</button>
            </div>
            {showAddCoworker && <AddCoworkerModal closeModal={setShowAddCoworker} refreshTabs={refreshTabs} />}
            {content}
        </div>
    );
    
    async function fetchRunningTabs() {
        //Set to empty array to trigger loading splash.
        setCoworkerTabs([]);
        const response = await fetch('tab');
        if (response.ok) {
            const data = await response.json();
            setCoworkerTabs(data.coworkerTabs);
            setRevision(data.revision);
        }
    }
}

export default App;