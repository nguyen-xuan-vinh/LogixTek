import React, { useState, useEffect } from "react";
import { getCurrentUser } from "../services/user.service";
import { getAll } from "../services/movie.service";
import IMovie from "../types/movie.type";

const Movie: React.FC = () => {
  const currentUser = getCurrentUser();
  const [content, setContent] = useState<Array<IMovie> | undefined>(undefined);

  useEffect(() => {
    getAll().then(
      (response: any) => {
        setContent(response.data);
      },
      (error: any) => {
        const _content =
          (error.response && error.response.data) ||
          error.message ||
          error.toString();

        setContent(_content);
      }
    );
  }, []);

  const loop = () => {
    {
      content?.map((movie, index) => { <h2>{movie.title}</h2> }
      )
    }
  }

  return (
    <div className="container">
      <header className="jumbotron">
        <>
          {loop()}
        </>
      </header>
    </div>
  );
};

export default Movie;