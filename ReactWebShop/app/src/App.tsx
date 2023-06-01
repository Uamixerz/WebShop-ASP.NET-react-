import React from 'react';
import logo from './logo.svg';
import './App.css';
import DefaultHeader from './components/containers/default/DefaultHeader';
import { Route, Routes } from 'react-router-dom';
import CategoryCreatePage from './components/containers/category/create/CategoryCreatePage';
import RegisterPage from './components/containers/auth/register/RegisterPage';

function App() {
  return (
    <>
      <DefaultHeader/>
      <Routes>
        
          <Route path='categories/create' element={<CategoryCreatePage/>}/>
          <Route path='register' element={<RegisterPage/>}/>
      </Routes>
    </>
  );
}

export default App;
