import { initializeApp } from 'firebase/app'
import { getFirestore, collection, getDocs,
         addDoc, deleteDoc, doc  
} from 'firebase/firestore'

const firebaseConfig = {
    apiKey: "AIzaSyDedizygSTUFeQbry1VPsnFbRHscYaCnog",
    authDomain: "dxmood-80262.firebaseapp.com",
    projectId: "dxmood-80262",
    storageBucket: "dxmood-80262.appspot.com",
    messagingSenderId: "455609541602",
    appId: "1:455609541602:web:97db5e06acbbbdb186cfbb",
    measurementId: "G-5CGZXHC3RX"
  }

//initialize firebase
initializeApp(firebaseConfig)

//init services
const db = getFirestore()

//collection reference 
const colRef = collection(db, 'Patients')

//get collection data
getDocs(colRef)
  .then((snapshot) => {
    let Patients = []
    snapshot.docs.forEach((doc)=> {
    Patients.push({ ...doc.data(), id: doc.id })
  }) 
  console.log(Patients)
})

.catch(err => {
  console.log(err.message)
})

//adding docs
const addPatientForm = document.querySelector( '.add')
addSongForm.addEventListener('submit', (e) => {
  e.preventDefault()
  addDoc(colRef, {
    title: addPatientForm.title.value
  })
  .then(() => {
    addPatientForm.reset()
  })

})

//delete docs
const deletePatientForm = document.querySelector( '.delete')
deletePatientForm.addEventListener('submit', (e) => {
  e.preventDefault()

  const docRef = doc(db, 'Patient', deletePatientForm.id.value)
  
  deleteDoc(docRef)
    .then(()=> {
        deletePatientForm.reset()
    })

})