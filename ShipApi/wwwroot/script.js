const shipForm = document.getElementById('shipForm');
const codeInput = document.getElementById('code');
const nameInput = document.getElementById('name');
const lengthInput = document.getElementById('length');
const widthInput = document.getElementById('width');
const messageArea = document.getElementById('message');
const shipTableBody = document.getElementById('shipTableBody');
const addShipButton = document.getElementById('addShipButton');
const updateShipButton = document.getElementById('updateShipButton');
const cancelShipButton = document.getElementById('cancelShipButton');

let editIndex = null;
let ships = [];

function addOrUpdateShip(isUpdate = false) {
    const code = codeInput.value.trim();
    const name = nameInput.value.trim();
    const length = parseInt(lengthInput.value);
    const width = parseInt(widthInput.value);

    if (!isValidInput(code, name, length, width)) {
        return;
    }

    if (isUpdate) {
        const xhr = new XMLHttpRequest();
        xhr.open('PUT', `http://localhost:5062/api/v1/ships/${code}`, true);
        xhr.setRequestHeader('Content-Type', 'application/json');

        xhr.onload = function () {
            if (xhr.status === 200) {
                ships[editIndex] = { code, name, length, width };
                clearForm();
                renderShips();
                editIndex = null;
                addShipButton.style.display = 'inline-block';
                cancelShipButton.style.display = 'none';
                updateShipButton.style.display = 'none';
                messageArea.textContent = 'Ship updated successfully.';
            } else {
                if (xhr.responseText) {
                    const msg = JSON.parse(xhr.responseText);
                    if (msg && msg.Message) {
                        messageArea.textContent = msg.Message;
                    } else {
                        messageArea.textContent = 'Error updating ship. Please try again.';
                    }
                }
                else {
                    messageArea.textContent = 'Error updating ship. Please try again.';
                }
            }
        };

        xhr.onerror = function () {
            messageArea.textContent = 'Error updating ship. Please try again.';
        };

        const shipData = { code, name, length, width };
        xhr.send(JSON.stringify(shipData));
    } else {
        const xhr = new XMLHttpRequest();
        xhr.open('POST', 'http://localhost:5062/api/v1/ships', true);
        xhr.setRequestHeader('Content-Type', 'application/json');

        xhr.onload = function () {
            if (xhr.status === 200) {
                const ship = { code, name, length, width };
                ships.push(ship);
                clearForm();
                renderShips();
                messageArea.textContent = 'Ship added successfully.';
            } else {
                if (xhr.responseText) {
                    const msg = JSON.parse(xhr.responseText);
                    if (msg && msg.Message) {
                        messageArea.textContent = msg.Message;
                    } else {
                        messageArea.textContent = 'Error adding ship. Please try again.';
                    }
                }
                else {
                    messageArea.textContent = 'Error adding ship. Please try again.';
                }
            }
        };

        xhr.onerror = function () {
            messageArea.textContent = 'Error adding ship. Please try again.';
        };

        const shipData = { code, name, length, width };
        xhr.send(JSON.stringify(shipData));
    }
}
function cancelUpdate() {
    updateShipButton.style.display = 'none';
    cancelShipButton.style.display = 'none';
    addShipButton.style.display = 'inline-block';
    clearForm();
}

function renderShips() {
    shipTableBody.innerHTML = '';

    ships.forEach((ship, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
                <td>${ship.code}</td>
                <td>${ship.name}</td>
                <td>${ship.length}</td>
                <td>${ship.width}</td>
                <td>
                    <button onclick="editShip(${index})">Edit</button>
                    <button onclick="removeShip(${index})">Remove</button>
                </td>
            `;
        shipTableBody.appendChild(row);
    });
}

function editShip(index) {
    const ship = ships[index];
    codeInput.value = ship.code;
    nameInput.value = ship.name;
    lengthInput.value = ship.length;
    widthInput.value = ship.width;
    editIndex = index;
    addShipButton.style.display = 'none';
    updateShipButton.style.display = 'inline-block';
    cancelShipButton.style.display = 'inline-block';
}

function removeShip(index) {
    const xhr = new XMLHttpRequest();
    xhr.open('DELETE', `http://localhost:5062/api/v1/ships/${ships[index].code}`, true);

    xhr.onload = function () {
        if (xhr.status === 200) {
            ships.splice(index, 1);
            renderShips();
            messageArea.textContent = 'Ship removed successfully.';
        } else {
            messageArea.textContent = 'Error removing ship. Please try again.';
        }
    };

    xhr.onerror = function () {
        messageArea.textContent = 'Error removing ship. Please try again.';
    };

    xhr.send();
}

function isValidInput(code, name, length, width) {
    const codeRegex = /^[A-Za-z]{4}-\d{4}-[A-Za-z]\d$/;

    if (!codeRegex.test(code)) {
        messageArea.textContent = 'Please enter a valid code in the format XXXX-0000-X0.';
        return false;
    }
    if (!name) {
        messageArea.textContent = 'Please enter a name for the ship.';
        return false;
    }
    if (length <= 0 || width <= 0 || !length || !width) {
        messageArea.textContent = 'Length and Width must be greater than 0.';
        return false;
    }
    return true;
}

function clearForm() {
    codeInput.value = '';
    nameInput.value = '';
    lengthInput.value = '';
    widthInput.value = '';
}

function loadShips() {
    const xhr = new XMLHttpRequest();
    xhr.open('GET', 'http://localhost:5062/api/v1/ships', true);

    xhr.onload = function () {
        if (xhr.status === 200) {
            ships = JSON.parse(xhr.responseText);
            renderShips();
        } else {
            messageArea.textContent = 'Error loading ships. Please try again.';
        }
    };

    xhr.onerror = function () {
        messageArea.textContent = 'Error loading ships. Please try again.';
    };

    xhr.send();
}


window.onload = function () {
    loadShips();
};