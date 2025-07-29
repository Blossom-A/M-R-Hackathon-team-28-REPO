import React from "react";
import { Link } from "react-router";
import './dashboardNav.css'

function dashboardNav() {
  return (
    <>
      <header>
        <div className={`app-name`}>App Name</div>
        <div className={`button-img-container`}>
          <img
            src="https://via.placeholder.com/32"
            alt="Profile"
            className={`img-container`}
          />
          <button
            className={`sign-out-button`}
          >
            Sign Out
          </button>
        </div>
      </header>
      <aside
        className={`sidnav`}
      >
        <nav>
          <ul style={{ listStyle: "none", padding: 0 }}>
            <li>
              <Link to="/">Dashboard</Link>
            </li>
            <li>
              <Link to="/">Profile</Link>
            </li>
            <li>
              <Link to="/">Logout</Link>
            </li>
            <li>
              <Link to="/">Cruds</Link>
            </li>
          </ul>
        </nav>
      </aside>
    </>
  );
}

export default dashboardNav;
