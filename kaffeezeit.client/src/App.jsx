import { useEffect, useState } from 'react';
import './App.css';
import CoworkerBox from './CoworkerBox';
import AddCoworkerModal from './AddCoworkerModal';

function App() {
    const [mode, setMode] = useState(modes.LOADING);
    const [coworkerTabs, setCoworkerTabs] = useState();
    const [coworkerStates, setCoworkerStates] = useState();
    const [revision, setRevision] = useState();
    const [isCurrentOrderPaid, setIsCurrentOrderPaid] = useState();
    const [showAddCoworker, setShowAddCoworker] = useState();
    const [errorMessage, setErrorMessage] = useState();

    useEffect(() => {
        fetchRunningTabs();
    }, []);

    const refreshTabs = () => {
        fetchRunningTabs();
    }

    const setInitialState = (coworkerTabs) => {
        let states = {};
        for (let tab of coworkerTabs) {
            var state = {
                id: tab.coworkerId,
                name: tab.coworkerName,
                isNext: tab.isNextToPay,
                runningTab: tab.runningTab,
                isSelected: false,
                isUseFavoriteSelected: true,
                favoriteDrinkCost: tab.favoriteDrinkCost,
                drinkCost: 0
            }
            states[state.id] = state;
        }
        setCoworkerStates(states);
    }

    const onCoworkerStateChange = (coworkerState) => {
        let copy = Object.assign({}, coworkerStates);
        copy[coworkerState.id] = coworkerState;
        setCoworkerStates(copy);
    }

    const onAddCoworker = (value) => {
        setShowAddCoworker(value);
    }

    const onRemoveCoworker = () => {
        setMode(modes.REMOVE);
    }

    const getSelected = () => {
        return Object.entries(coworkerStates).filter(([key, value]) => value.isSelected);
    }

    const onRemoveConfirm = () => {
        const coworkersIdsToRemove = getSelected().map(([key, value]) => key);
        deleteCoworkers(coworkersIdsToRemove);
        setMode(modes.VIEW);
    }

    const onOrderSubmit = async () => {
        const convertToOrder = (key, value) => {
            return {
                coworkerId: key,
                useFavorite: value.isUseFavoriteSelected,
                drinkCost: value.isUseFavoriteSelected ? 0 : value.drinkCost
            }
        }
        let orders = getSelected().map(([key, value]) => convertToOrder(key, value));
        const request = {
            orders: orders,
            revision: revision
        }
        await submitOrderRequest(request);
    }

    const onChoosePayer = () => {
        let copy = Object.assign({}, coworkerStates);
        for (let id in copy) {
            copy[id].isSelected = copy[id].isNext;
        }
        setCoworkerStates(copy);
        setMode(modes.PAYING)
    }

    const onPay = () => {
        var selected = getSelected();
        if (selected.length !== 1) {
            setErrorMessage("Cannot select more than one payer.");
            return;
        }
        const payer = selected[0][1];
        const overrideNext = !payer.isNext;
        const paymentRequest = {
            coworkerId: payer.id,
            overrideNext: overrideNext
        }
        setErrorMessage(null);
        submitPaymentRequest(paymentRequest);
    }

    const onCancel = () => {
        if (!isCurrentOrderPaid) {
            setMode(modes.PAY);
        }
        else {
            setMode(modes.VIEW);
        }
    }

    return (
        <div>
            <h1 id="mainheader">It's Coffee Time!</h1>
            <div>
                <button style={{ backgroundColor: "seagreen" }} onClick={() => onAddCoworker(true)}>Add Coworker</button>
                <button style={{ backgroundColor: "maroon" }} onClick={onRemoveCoworker }>Remove Coworker</button>
            </div>
            {showAddCoworker && <AddCoworkerModal closeModal={setShowAddCoworker} refreshTabs={refreshTabs} />}
            {showOnMode(mode, modes.LOADING) && <div>Loading...</div> }
            {showOnMode(mode, modes.VIEW, modes.ORDER, modes.REMOVE, modes.PAY, modes.PAYING) && <div>
                {coworkerTabs?.map(coworkerTab => <CoworkerBox
                    key={coworkerTab.coworkerId}
                    mode={mode}
                    coworkerState={coworkerStates[coworkerTab.coworkerId]}
                    onChange={onCoworkerStateChange} />)}
            </div>}
            <div>
                {errorMessage}
            </div>
            <div>
                {showOnMode(mode, modes.VIEW) && <button style={{ backgroundColor: "seagreen" }} onClick={() => setMode(modes.ORDER)}>New Order</button>}
                {showOnMode(mode, modes.PAY) && <button style={{ backgroundColor: "seagreen" }} onClick={onChoosePayer}>Choose Payer</button>}
                {showOnMode(mode, modes.PAYING) && <button style={{ backgroundColor: "seagreen" }} onClick={onPay}>Pay</button>}
                {showOnMode(mode, modes.ORDER) && <button style={{ backgroundColor: "seagreen" }} onClick={onOrderSubmit}>Submit</button> }
                {showOnMode(mode, modes.REMOVE) && <button style={{ backgroundColor: "seagreen" }}  onClick={onRemoveConfirm} >Submit</button>}
                {showOnMode(mode, modes.REMOVE, modes.ORDER, modes.PAYING) && <button style={{ backgroundColor: "maroon" }} onClick={onCancel} >Cancel</button>}
            </div>
        </div>
    );
    
    async function fetchRunningTabs() {
        if (mode !== modes.LOADING)
            setMode(modes.LOADING);
        const response = await fetch('tab');
        if (response.ok) {
            const data = await response.json();
            setCoworkerTabs(data.coworkerTabs);
            setRevision(data.revision);
            setIsCurrentOrderPaid(data.isCurrentOrderPaid);
            setInitialState(data.coworkerTabs);
            if (!data.isCurrentOrderPaid) {
                setMode(modes.PAY);
            }
            else {
                setMode(modes.VIEW);
            }
            
        }
        
    }

    async function deleteCoworkers(coworkerIdsToDelete) {
        if (mode !== modes.LOADING)
            setMode(modes.LOADING);
        for (let coworkerId of coworkerIdsToDelete) {
            const url = 'coworker/' + coworkerId;
            const data = await fetch(url, {
                method: "DELETE"
            });
        }
        await fetchRunningTabs();
    }

    async function submitOrderRequest(orderRequest) {
        if (mode !== modes.LOADING)
            setMode(modes.LOADING);
        const response = await fetch('tab', {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(orderRequest)
        });
        if (response.ok) {
            await fetchRunningTabs();
        }
        else {
            // TODO display error.
        }
    }

    async function submitPaymentRequest(paymentRequest) {
        if (mode !== modes.LOADING)
            setMode(modes.LOADING);
        const response = await fetch('tab/pay', {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(paymentRequest)
        });
        if (response.ok) {
            await fetchRunningTabs();
        }
        else {
            // TODO display error.
        }
    }
}

export function showOnMode(mode, ...includeModes) {
    return includeModes.includes(mode);
}

export const modes = {
    LOADING: 'loading',
    VIEW: 'view',
    REMOVE: 'remove',
    ORDER: 'order',
    PAY: 'pay',
    PAYING: 'paying'
};

export default App;