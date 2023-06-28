import React from 'react';
import logo from './logo.svg';
import './App.css';
import DefaultHeader from './components/containers/default/DefaultHeader';
import { Route, Routes } from 'react-router-dom';
import CategoryCreatePage from './components/containers/category/create/CategoryCreatePage';
import RegisterPage from './components/containers/auth/register/RegisterPage';
import LoginPage from './components/containers/auth/login/LoginPage';
import CategoryEditPage from './components/containers/category/edit/CategoryEditPage';
import AdminPage from './components/containers/admin/AdminPage';


function App() {
  return (
    <>
      <DefaultHeader/>
      <Routes>
          <Route path='login' element={<LoginPage/>}/>
          <Route path='categories/edit/:id' element={<CategoryEditPage/>}/>
          <Route path='categories/create' element={<CategoryCreatePage/>}/>
          <Route path='register' element={<RegisterPage/>}/>
          <Route path='admin' element={<AdminPage/>}/>
      </Routes>
    </>
  );
}

export default App;
