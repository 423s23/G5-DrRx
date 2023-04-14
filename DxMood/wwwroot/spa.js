const serviceURL = "http://localhost:5191";

let pages = ["login", "patients", "addpatient", "account"];

const loginPage = document.getElementById("login");
const patientsPage = document.getElementById("patients");
const addpatientPage = document.getElementById("addpatient");
const accountPage = document.getElementById("account");

let currentPage = pages[0];

function changePage(page){
    currentPage = pages[page];

    loginPage.style.display = currentPage == "login" ? "flex" : "none";
    patientsPage.style.display = currentPage == "patients" ? "flex" : "none";
    addpatientPage.style.display =
        currentPage == "addpatient" ? "flex" : "none";
    accountPage.style.display = currentPage == "account" ? "flex" : "none";
};

changePage(0);

//END PAGE STUFF

const loginButton = document.getElementById("login-button");

const username = document.getElementById("username");
const password = document.getElementById("password");

const patientListDiv = document.getElementById("patient-list");

loginButton.onclick = (e) => {
    e.preventDefault();
    signInDoctor(username.value, password.value);
    changePage(1);
};

//Real time shit now

let patientList = [];

function addNewPatient(name, diagnosis) {
    patientList.push({
        name: name,
        diagnosis: diagnosis,
    });
    updatePatientDiv();
}

addNewPatient("Test1", "He dead");
addNewPatient("Test2", "He dead");
addNewPatient("Test3", "He dead");

let loginInfo = {
    username: "Testing123",
    password: "password123",
};

function updatePatientDiv() {
    patientListDiv.innerHTML = "";
    for (let i = 0, e = patientList.length; i < e; i++) {
        patientListDiv.innerHTML += `<li>${patientList[i]?.name} <span class="diagnosis">Diagnosis: ${patientList[i]?.diagnosis}</span></li>`;
    }
}

const refreshPatientButton = document.getElementById("refresh-patients-btn");

refreshPatientButton.onclick = (e) => {
    e.preventDefault();
    updatePatientDiv();
    console.log("dodo");
};

const doctorName = document.getElementById("doctor-name");
const numberPatients = document.getElementById("number-patients");

function updateDoctorName(lastname){
    doctorName.innerText = `Dr. ${lastname}'s profile`;
    numberPatients.innerText = `${patientList.length || 0} patients`;
};

function signInDoctor (username, password){
    loginInfo.username = username;
    loginInfo.password = password;
    updateDoctorName(username);
    APIsignInDoctor(username,password);
};

const diagnoseButton = document.getElementById("diagnose");
const inputName = document.getElementById("inp-name");
const inputDOB = document.getElementById("inp-dob");

diagnoseButton.onclick = (e) => {
    e.preventDefault();
    //createPatient("testfirst", "testlast", "1/23/2002", doctorID);
    addNewPatient(inputName.value, "AIDS");
    updateDoctorName();
    APIcreatePatient(inputName.value,inputName.value,inputDOB.value,doctorID);
};

//API calls

let doctorID = "";


const APIsignInDoctor = (username, password) => {
    let body = {
        username: username,
        password: password,
    };
    axios
        .post(`${serviceURL}/doctor/login`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`POST doctor login`, response);
            doctorID = response?.id;
        })
        .catch((error) => console.error(error));
};

const APIcreatePatient = (firstname, lastname, DOB, doctorID) => {
    let body = {
        id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        lastName: lastname,
        firstName: firstname,
        dateOfBirth: DOB,
        doctorId: doctorID,
    };
    axios
        .post(`${serviceURL}/patient`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`PUT patient`, response);
        })
        .catch((error) => console.error(error));
};