import React, { useState, useEffect } from "react";

import { getAll } from "../services/movie.service";

const Home: React.FC = () => {
  const [content, setContent] = useState<string>("");

  // useEffect(() => {
  //   getAll().then(
  //     (response) => {
  //       setContent(response.data);
  //     },
  //     (error) => {
  //       const _content =
  //         (error.response && error.response.data) ||
  //         error.message ||
  //         error.toString();

  //       setContent(_content);
  //     }
  //   );
  // }, []);

  return (
    <div className="container">
      <header className="jumbotron">
        <h3>{content}</h3>
      </header>
    </div>
  );
};

export default Home;