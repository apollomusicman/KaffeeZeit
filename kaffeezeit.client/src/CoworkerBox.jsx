import { useState } from 'react';
import { modes, showOnMode } from './App';

function CoworkerBox({ mode, coworkerState, onChange }) {

    const onBoxChange = (key, value) => {
        let updatedState = Object.assign({}, coworkerState);
        updatedState[key] = value;
        onChange(updatedState);
    }

    return (
        <div style={{backgroundColor: 'dimgrey', margin: "5px", borderRadius: "10px", minHeight: "40px"} }>
            {showOnMode(mode, modes.ORDER, modes.REMOVE, modes.PAYING) && <input style={{margin: "5px"}} type="checkbox" checked={coworkerState?.isSelected} onChange={(event) => onBoxChange("isSelected", event.target.checked)} />}
            <span style={{margin: "5px"} }>{coworkerState?.name}</span>
            {coworkerState?.isNext && <span style={{margin: "5px", padding: "0px 5px", backgroundColor: "darkgoldenrod", borderRadius: "5px"} }>next</span>}
            <label style={{margin: "5px"}} htmlFor="tab">tab:</label>
            <span style={{ margin: "5px" }} name="tab">${coworkerState?.runningTab}</span>
            <div>
                {showOnMode(mode, modes.ORDER) && <><label style={{ margin: "5px" }} htmlFor="favorite">Use Favorite drink?</label>
                    <input style={{ margin: "5px" }} type="checkbox" name="favorite" checked={coworkerState?.isUseFavoriteSelected} onChange={(event) => onBoxChange("isUseFavoriteSelected", event.target.checked)} /></>}
                {showOnMode(mode, modes.ORDER) && <><label style={{ margin: "5px" }} htmlFor="cost">Drink cost</label>
                    <input style={{ margin: "5px" }} type="number" name="cost" value={coworkerState?.isUseFavoriteSelected ? coworkerState?.favoriteDrinkCost : coworkerState?.drinkCost ?? 0.00} step={0.01} onChange={(event) => onBoxChange("drinkCost", event.target.value)} disabled={coworkerState?.isUseFavoriteSelected ? "disabled" : ""} /></>}
            </div>
        </div>
    )
}

export default CoworkerBox;