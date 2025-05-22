import { useState } from 'react';

function AddCoworkerModal({ closeModal, refreshTabs }) {
    const [name, setName] = useState();
    const [favoriteCost, setFavoriteCost] = useState();

    const onSubmit = () => {
        createCoworker();
        closeModal(false);
    };

    const onCancel = () => {
        closeModal(false);
    };

    const onNameChange = (event) => {
        setName(event.target.value);
    }

    const onFavoriteChange = (event) => {
        setFavoriteCost(event.target.value);
    }


    return (
        <div>
            <label htmlFor="name">Coworkers name</label>
            <input name="name" value={name} onChange={onNameChange}></input>
            <label htmlFor="favorite">Favorite drink cost</label>
            <input type="number" name="favorite" value={favoriteCost} onChange={onFavoriteChange}></input>
            <div>
                <button style={{ backgroundColor: "seagreen" }} onClick={onSubmit}>Submit</button>
                <button style={{ backgroundColor: "maroon" }} onClick={onCancel}>Cancel</button>
            </div>
        </div>
    );

    async function createCoworker() {
        const request = {
            name: name ,
            favoriteDrinkCost: favoriteCost
        }
        const response = await fetch('coworker', {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        });
        if (response.ok) {
            refreshTabs();
        }
        else {
            //TODO display error.
        }
    }
}

export default AddCoworkerModal;