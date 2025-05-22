import { useState } from 'react';

function CoworkerBox({ name, isNext, runningTab }) {
    const [isSelected, setIsSelected] = useState();
    const [isUseFavoriteSelected, setIsUseFavoriteSelected] = useState(true);
    const [drinkCost, setDrinkCost] = useState();

    const onCheckboxChange = (event) => {
        setIsSelected(event.target.checked);
    }

    const onFavoriteChange = (event) => {
        setIsUseFavoriteSelected(event.target.checked);
    }

    const onDrinkCostChange = (event) => {
        setDrinkCost(event.target.value);
    }

    return (
        <div>
            <input type="checkbox" checked={isSelected} onChange={onCheckboxChange}/>
            <div>{name}</div>
            {isNext && <div>next</div>}
            <div>${runningTab}</div>
            <label htmlFor="favorite">Use Favorite drink?</label>
            <input type="checkbox" name="favorite" checked={isUseFavoriteSelected} onChange={onFavoriteChange} />
            <label htmlFor="cost">Drink cost</label>
            <input type="number" name="cost" value={drinkCost ?? 0.00} step={0.01} onChange={onDrinkCostChange} disabled={isUseFavoriteSelected ? "disabled" : ""} />
        </div>
    )
}

export default CoworkerBox;