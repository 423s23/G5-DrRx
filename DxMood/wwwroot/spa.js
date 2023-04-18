const serviceURL = "http://localhost:5191";

let pages = ["login", "patients", "addpatient", "account"];

const loginPage = document.getElementById("login");
const patientsPage = document.getElementById("patients");
const addpatientPage = document.getElementById("addpatient");
const accountPage = document.getElementById("account");

let currentPage = pages[0];

function changePage(page) {
    currentPage = pages[page];

    loginPage.style.display = currentPage == "login" ? "flex" : "none";
    patientsPage.style.display = currentPage == "patients" ? "flex" : "none";
    addpatientPage.style.display =
        currentPage == "addpatient" ? "flex" : "none";
    accountPage.style.display = currentPage == "account" ? "flex" : "none";
}

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

function updateDoctorName(lastname) {
    doctorName.innerText = `Dr. ${lastname}'s profile`;
    numberPatients.innerText = `${patientList.length || 0} patients`;
}

function updatePatientList(){

    patientListAPI.forEach((patient)=>{
        console.log(patient);
    })

    //updatePatientDiv();
}

function signInDoctor(username, password) {
    loginInfo.username = username;
    loginInfo.password = password;
    updateDoctorName(username);
    APIsignInDoctor(username, password);
}

const diagnoseButton = document.getElementById("diagnose");
const inputName = document.getElementById("inp-name");
const inputDOB = document.getElementById("inp-dob");

diagnoseButton.onclick = (e) => {
    e.preventDefault();
    //createPatient("testfirst", "testlast", "1/23/2002", doctorID);
    addNewPatient(inputName.value, "MDD + Insomnia");
    updateDoctorName();
    APIcreatePatient(inputName.value, inputName.value, doctorID);
    changePage(1); // Change to patient page
};

//API calls

let doctorID = "";

let doctorObject = {};

let patientListAPI = [];

const APIsignInDoctor = (username, password) => {
    let body = {
        firstName: "Phillip",
        lastName: "Bain",
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
            doctorObject = response.data;
            doctorID = doctorObject.id;
            updateDoctorName(doctorObject.lastName);
        })
        .catch((error) => console.error(error));
};

const APIcreatePatient = (firstname, lastname, doctorID) => {
    let body = {
        id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        lastName: lastname,
        firstName: firstname,
        doctorId: doctorID,
        doctor: {
            lastName: "string",
            firstName: "string",
            userName: "string",
            password: "string",
        },
    };
    axios
        .post(`${serviceURL}/patient`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`POST patient`, response);
            let patientObject = response;
            APIcreateResult(patientObject.patientID, 0,0,0,12, "testing");
        })
        .catch((error) => console.error(error));
};

const APIgetDoctor = (doctorID) => {
    let body = {};
    axios
        .get(`${serviceURL}/doctor/${doctorID}`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`GET doctor`, response);
            doctorObject = response.data;
            doctorID = doctorObject.id;
            patientListAPI = doctorObject.patients;
            updateDoctorName(doctorObject.lastName);
            updatePatientList();
        })
        .catch((error) => console.error(error));
};

const APIcreateResult = (patientID, phq9, gad7, isi, asrs, note) => {
    let body = {
        phq9: phq9,
        gad7: gad7,
        isi: isi,
        asrs: asrs,
        diagnosis: "string",
        recommendedMedication: "string",
        note: note,
        patientId: patientID,
        patient: {
            lastName: "string",
            firstName: "string",
            doctor: {
                lastName: "string",
                firstName: "string",
                userName: "string",
                password: "string",
            },
        },
    };
    axios
        .post(`${serviceURL}/result`, body, {
            headers: {
                "Content-Type": "application/json",
            },
        })
        .then((response) => {
            console.log(`POST result`, response);
            resultObject = response.data;
            let diagnosis = resultObject.diagnosis;
            APIgetDoctor(doctorID);
            console.log(diagnosis)
        })
        .catch((error) => console.error(error));
};
